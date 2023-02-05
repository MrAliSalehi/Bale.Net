using System.Diagnostics.CodeAnalysis;

namespace Bale.Net.Types;

public class Media
{
    internal readonly MultipartFormDataContent Content;
    private Media(MultipartFormDataContent content)
    {
        Content = content;
    }
    public static Media FromDisk([StringSyntax(StringSyntaxAttribute.Uri)] string path)
    {
        var content = new MultipartFormDataContent();
        content.Add(new StreamContent(File.OpenRead(path)),"photo",Path.GetFileName(path));
        return new Media(content);
    }
    public static Media FromId()
    {
        var media = new Media(new MultipartFormDataContent());
        return media;
    }
    public static Media FromUrl(Uri url)
    {//todo test this
        var content = new MultipartFormDataContent();
        content.Add(new StringContent(url.AbsolutePath),"photo");
        return new Media(content);
    }
}