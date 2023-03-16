using System.Diagnostics.CodeAnalysis;

namespace Bale.Net.Types;

public class Media
{
    private readonly HttpContent _content;
    private bool _isFile;
    private Media(HttpContent content)
    {
        _content = content;
    }
    public static Media FromDisk([StringSyntax(StringSyntaxAttribute.Uri)] string path) => new(new StreamContent(System.IO.File.OpenRead(path)))
    {
        _isFile = true
    };
    public static Media FromId(string fileId) => new(new StringContent(fileId));
    public static Media FromUrl(Uri url) => new (new StringContent(url.AbsoluteUri));

    internal MultipartFormDataContent GetContent(string contentName)
    {
        var form = new MultipartFormDataContent();
        if (_isFile)
            form.Add(_content, contentName, "file");
        else
            form.Add(_content, contentName);
        return form;
    }
}