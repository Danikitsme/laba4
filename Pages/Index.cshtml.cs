using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Threading.Tasks;

public class IndexModel : PageModel
{
    private readonly IWebHostEnvironment _hostingEnvironment;

    public IndexModel(IWebHostEnvironment hostingEnvironment)
    {
        _hostingEnvironment = hostingEnvironment;
    }

    public void OnGet()
    {
        // Ваш код для GET-запиту (якщо потрібно)
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var file = Request.Form.Files["file"];

        if (file != null && file.Length > 0)
        {
            var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "Files");

            // Забезпечте існування директорії для завантаження файлів
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var filePath = Path.Combine(uploadsFolder, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Додайте ваш код обробки файлу (якщо потрібно)

            return RedirectToPage("/Index");
        }

        // Обробка помилок, якщо файл не був вибраний
        ModelState.AddModelError("File", "Виберіть файл для завантаження.");
        return Page();
    }
}
