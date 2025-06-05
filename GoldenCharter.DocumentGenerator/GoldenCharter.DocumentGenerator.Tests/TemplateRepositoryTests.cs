using GoldenCharter.DocumentGenerator.Core.Enums;
using GoldenCharter.DocumentGenerator.Core.Options;
using GoldenCharter.DocumentGenerator.Core.Repositories;
using Microsoft.Extensions.Options;

namespace GoldenCharter.DocumentGenerator.Tests;

public class TemplateRepositoryTests
{
    private static string CreateTempTemplateFile(DocumentType type, string content)
    {
        var dir = Path.Combine(Path.GetTempPath(), "template-tests", Guid.NewGuid().ToString());
        Directory.CreateDirectory(dir);
        File.WriteAllText(Path.Combine(dir, $"{type.ToString().ToLowerInvariant()}.html"), content);
        return dir;
    }

    [Theory]
    [InlineData(DocumentType.Dan)]
    [InlineData(DocumentType.Idd)]
    public async Task GetTemplateAsync_ReturnsExpectedContent_ForSupportedTypes(DocumentType type)
    {
        var content = $"<html>{{{{Name}}}} for {type}</html>";
        var path = CreateTempTemplateFile(type, content);
        var repo = new TemplateRepository(Options.Create(new TemplateOptions { TemplatePath = path }));

        var result = await repo.GetTemplateAsync(type);

        Assert.Equal(content, result);
    }


    [Fact]
    public async Task GetTemplateAsync_ReturnsExpectedContent()
    {
        var content = "<html>{{Name}}</html>";
        var path = CreateTempTemplateFile(DocumentType.Dan, content);
        var repo = new TemplateRepository(Options.Create(new TemplateOptions { TemplatePath = path }));

        var result = await repo.GetTemplateAsync(DocumentType.Dan);

        Assert.Equal(content, result);
    }

    [Fact]
    public async Task GetTemplateAsync_Throws_WhenNotFound()
    {
        var path = Path.Combine(Path.GetTempPath(), "missing-dir");
        var repo = new TemplateRepository(Options.Create(new TemplateOptions { TemplatePath = path }));

        await Assert.ThrowsAsync<FileNotFoundException>(() => repo.GetTemplateAsync(DocumentType.Idd));
    }

    [Fact]
    public void Constructor_Throws_WhenNullOptions()
    {
        Assert.Throws<ArgumentNullException>(() => new TemplateRepository(null!));
    }
}
