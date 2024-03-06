using EpochApp.Client.Services;
using EpochApp.Shared;
using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using Severity=MudBlazor.Severity;

namespace EpochApp.Client.Shared.Forms
{
    /// <summary>
    /// The file upload component.
    /// </summary>
    public partial class FileUpload
    {
        private const string FileContent = "this is content";
        private const string DefaultDragClass = "relative rounded-lg border-2 border-dashed pa-4 mt-4 mud-width-full mud-height-full z-10";
        private string _dragClass = DefaultDragClass;
        private FileModel _fileModel = new FileModel();
        private bool _isTouched;
        private bool _isValid;
        private FileModelFluentValidator _validationRules = new FileModelFluentValidator();

        /// <summary>
        /// The files are flagged for the world if true.
        /// </summary>
        [Parameter] public bool IsWorldFile { get; set; } = false;
        [CascadingParameter] private WorldDTO ActiveWorld { get; set; }
        [Inject] private EpochAuthProvider Auth { get; set; }
        [Inject] private IFileService FileService { get; set; }

        private async Task Upload()
        {
            // Upload the files here
            Snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopCenter;
            Snackbar.Add("Uploading files...", Severity.Info);
            var processedFiles = 0;
            if (_isValid)
            {
                foreach (var file in _fileModel.Files)
                {
                    var buffer = new byte[file.Size];
                    _ = await file.OpenReadStream(maxAllowedSize: 10485760).ReadAsync(buffer);
                    var data = Convert.ToBase64String(buffer);
                    var newFile = new FileUploadDTO
                                  {
                                      FileName = file.Name,
                                      FileSize = file.Size,
                                      FileData = data,
                                      Alias = null,
                                      UserId = Auth.CurrentUser.UserID,
                                      WorldId = null
                                  };
                    if (IsWorldFile)
                    {
                        newFile.WorldId = ActiveWorld?.WorldId;
                        await FileService.UploadFileAsync(Auth.CurrentUser.UserID, ActiveWorld.WorldId, newFile);
                    }
                    else
                    {
                        await FileService.UploadFileAsync(Auth.CurrentUser.UserID, newFile);
                    }
                    processedFiles += 1;
                    //_percentDone = processedFiles / _uploadedFiles.Count;
                    await Task.Delay(200);
                    StateHasChanged();
                }
            }
        }

        private void SetDragClass() => _dragClass = $"{DefaultDragClass} mud-border-primary";

        private void ClearDragClass() => _dragClass = DefaultDragClass;

        /// <summary>
        /// FluentValidation rules for the file upload component.
        /// </summary>
        public class FileModelFluentValidator : AbstractValidator<FileModel>
        {
            /// <inheritdoc />
            public FileModelFluentValidator()
            {
                RuleFor(x => x.Files)
                    .NotEmpty()
                    .WithMessage("There must be at least 1 file.");
                RuleForEach(x => x.Files)
                    .NotEmpty()
                    .WithMessage("The file cannot be empty.");
                When(x => x.Files.Count > 0, () =>
                {
                    RuleForEach(x => x.Files)
                        .Must(x => x.Size <= 10485760)
                        .WithMessage("The file size must be less than 10MB.");
                });
            }

            /// <summary>
            /// Validate the value of the file being uploaded.
            /// </summary>
            public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
            {
                var result = await ValidateAsync(ValidationContext<FileModel>.CreateWithOptions((FileModel)model, x => x.IncludeProperties(propertyName)));
                return result.IsValid
                           ? Array.Empty<string>()
                           : result.Errors.Select(e => e.ErrorMessage);
            };
        }

        /// <summary>
        /// Model for the file upload component.
        /// </summary>
        public class FileModel
        {
            /// <summary>
            /// The files to be uploaded.
            /// </summary>
            public IReadOnlyList<IBrowserFile> Files { get; set; } = new List<IBrowserFile>();
        }
    }
}