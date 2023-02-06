using System.Diagnostics.CodeAnalysis;

namespace Bale.Net.Types;

public class Media
{
    private readonly HttpContent _content;
    private Media(HttpContent content)
    {
        _content = content;
    }
    public static Media FromDisk([StringSyntax(StringSyntaxAttribute.Uri)] string path) => new(new StreamContent(System.IO.File.OpenRead(path)));
    public static Media FromId(string fileId) => new(new StringContent(fileId));
    public static Media FromUrl(Uri url) => new(new StringContent(url.AbsoluteUri));
    
    internal MultipartFormDataContent GetContent(string contentName)
    {
        var form = new MultipartFormDataContent();
        form.Add(_content,contentName,"file");
        return form;
    }
}