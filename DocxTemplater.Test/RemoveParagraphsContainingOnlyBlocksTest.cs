using DocxTemplater.Images;

namespace DocxTemplater.Test
{
    internal class RemoveParagraphsContainingOnlyBlocksTest
    {


        [Test]
        public void TestTemplateDocumentWithAllBlockTypes()
        {
            // Load test image
            var imageBytes = File.ReadAllBytes("Resources/testImage.jpg");

            using var fileStream = File.OpenRead("Resources/RemoveParagraphsContainingOnlyBlocks.docx");
            var docTemplate = new DocxTemplate(fileStream);

            // Register the image formatter
            docTemplate.RegisterFormatter(new ImageFormatter());


            // Create test data that matches the template structure
            var testData = new
            {
                Val = "Test Value",
                Items = new[] { "Item 1", "Item 2" },
                NoItems = Array.Empty<string>(),
                Models = new[]
                {
                    new { Header = "First Header", Text = "This is the first text block with some detailed content" },
                    new { Header = "Second Header", Text = "Another text block with different content" },
                    new { Header = "Third Header", Text = "Yet another block of text to test the template" },
                    new { Header = "Fourth Header", Text = "Final text block with unique content" }
                },
                MyBool = true,
                MyOtherBool = false,
                MyString = "Hello, World!",
                MyNumber = 42,
                // Add images data with the loaded image
                Images = new[]
                {
                    new { Data = imageBytes },
                    new { Data = imageBytes }
                }
            };

            docTemplate.BindModel("", testData);
            var result = docTemplate.Process();
            docTemplate.Validate();

            // Save the output file for manual inspection
            // Save for visual inspection if needed
            var outputPath = Path.GetFullPath("RemoveParagraphsContainingOnlyBlocks_Output.docx");
            using (var fs = File.Create(outputPath))
            {
                result.CopyTo(fs);
            }
            Console.WriteLine($"Output file saved to: {outputPath}");

        }

        [Test]
        public void TestTemplateDocumentWithMultipleImages()
        {
            // Load test images
            var image1 = File.ReadAllBytes("Resources/testImage.jpg");
            var image2 = File.ReadAllBytes("Resources/testImage_rot.jpg");

            using var fileStream = File.OpenRead("Resources/RemoveParagraphsContainingOnlyBlocks.docx");
            var docTemplate = new DocxTemplate(fileStream);

            // Register the image formatter
            docTemplate.RegisterFormatter(new ImageFormatter());

            // Create test data with multiple images
            var testData = new
            {
                Val = "Test Value with Multiple Images",
                Items = new[] { "Item 1", "Item 2", "Item 3" },
                NoItems = Array.Empty<string>(),
                MyBool = true,
                MyOtherBool = false,
                MyString = "Hello, World!",
                MyNumber = 42,
                // Add multiple images in the array
                Images = new[]
                {
                    new { Data = image1 },
                    new { Data = image2 }
                }
            };

            docTemplate.BindModel("", testData);
            var result = docTemplate.Process();
            docTemplate.Validate();

            // Save the output file for manual inspection
            var outputPath = Path.GetFullPath("RemoveParagraphsContainingOnlyBlocks_MultipleImages_Output.docx");
            using (var fs = File.Create(outputPath))
            {
                result.CopyTo(fs);
            }
            Console.WriteLine($"Output file saved to: {outputPath}");
        }
    }
}
