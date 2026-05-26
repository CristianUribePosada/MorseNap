using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MorseNap.Structures;

namespace MorseNap.Pages
{
    public class IndexModel : PageModel
    {
        private readonly MorseTree _morseTree;

        public IndexModel()
        {
            _morseTree = new MorseTree();
        }

        [BindProperty] public string InputText { get; set; }
        [BindProperty] public string InputMorse { get; set; }
        public string ErrorMessage { get; set; }

        public void OnGet() { }

        public void OnPostTranslateText(string inputText)
        {
            InputText = inputText;
            InputMorse = _morseTree.TranslateToMorse(inputText);
        }

        public void OnPostTranslateMorse(string inputMorse)
        {
            InputMorse = inputMorse;
            string error;
            string result = _morseTree.TranslateToText(inputMorse, out error);

            if (error != null)
            {
                ErrorMessage = error;
            }
            else
            {
                InputText = result;
            }
        }

        public async Task<IActionResult> OnPostUploadFileAsync(IFormFile upload)
        {
            if (upload != null && upload.Length > 0)
            {
                using (var reader = new StreamReader(upload.OpenReadStream()))
                {
                    string fileContent = await reader.ReadToEndAsync();

                    if (fileContent.Contains('.') || fileContent.Contains('-'))
                    {
                        InputMorse = fileContent;
                        OnPostTranslateMorse(fileContent);
                    }
                    else
                    {
                        InputText = fileContent;
                        OnPostTranslateText(fileContent);
                    }
                }
            }
            return Page();
        }

        public IActionResult OnPostDownloadFile(string contentToDownload)
        {
            if (string.IsNullOrEmpty(contentToDownload)) return RedirectToPage();

            var bytes = Encoding.UTF8.GetBytes(contentToDownload);
            return File(bytes, "text/plain", "transmision_morsenap.txt");
        }
    }
}