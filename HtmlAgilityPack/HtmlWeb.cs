// Decompiled with JetBrains decompiler
// Type: HtmlAgilityPack.HtmlWeb
// Assembly: HtmlAgilityPack, Version=1.4.6.0, Culture=neutral, PublicKeyToken=bd319b19eaf3b43a
// MVID: 83185D3B-3939-439C-A54F-260F9279D9C8
// Assembly location: D:\Program Files (x86)\Huion Tablet\Release\HtmlAgilityPack.dll

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace HtmlAgilityPack
{
  public class HtmlWeb
  {
    private bool _autoDetectEncoding = true;
    private HttpStatusCode _statusCode = HttpStatusCode.OK;
    private int _streamBufferSize = 1024;
    private string _userAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:x.x.x) Gecko/20041107 Firefox/x.x";
    private bool _cacheOnly;
    private string _cachePath;
    private bool _fromCache;
    private int _requestDuration;
    private Uri _responseUri;
    private bool _useCookies;
    private bool _usingCache;
    public HtmlWeb.PostResponseHandler PostResponse;
    public HtmlWeb.PreHandleDocumentHandler PreHandleDocument;
    public HtmlWeb.PreRequestHandler PreRequest;
    private static Dictionary<string, string> _mimeTypes;
    private Encoding _encoding;

    internal static Dictionary<string, string> MimeTypes
    {
      get
      {
        if (HtmlWeb._mimeTypes != null)
          return HtmlWeb._mimeTypes;
        HtmlWeb._mimeTypes = new Dictionary<string, string>();
        HtmlWeb._mimeTypes.Add(".3dm", "x-world/x-3dmf");
        HtmlWeb._mimeTypes.Add(".3dmf", "x-world/x-3dmf");
        HtmlWeb._mimeTypes.Add(".a", "application/octet-stream");
        HtmlWeb._mimeTypes.Add(".aab", "application/x-authorware-bin");
        HtmlWeb._mimeTypes.Add(".aam", "application/x-authorware-map");
        HtmlWeb._mimeTypes.Add(".aas", "application/x-authorware-seg");
        HtmlWeb._mimeTypes.Add(".abc", "text/vnd.abc");
        HtmlWeb._mimeTypes.Add(".acgi", "text/html");
        HtmlWeb._mimeTypes.Add(".afl", "video/animaflex");
        HtmlWeb._mimeTypes.Add(".ai", "application/postscript");
        HtmlWeb._mimeTypes.Add(".aif", "audio/aiff");
        HtmlWeb._mimeTypes.Add(".aif", "audio/x-aiff");
        HtmlWeb._mimeTypes.Add(".aifc", "audio/aiff");
        HtmlWeb._mimeTypes.Add(".aifc", "audio/x-aiff");
        HtmlWeb._mimeTypes.Add(".aiff", "audio/aiff");
        HtmlWeb._mimeTypes.Add(".aiff", "audio/x-aiff");
        HtmlWeb._mimeTypes.Add(".aim", "application/x-aim");
        HtmlWeb._mimeTypes.Add(".aip", "text/x-audiosoft-intra");
        HtmlWeb._mimeTypes.Add(".ani", "application/x-navi-animation");
        HtmlWeb._mimeTypes.Add(".aos", "application/x-nokia-9000-communicator-add-on-software");
        HtmlWeb._mimeTypes.Add(".aps", "application/mime");
        HtmlWeb._mimeTypes.Add(".arc", "application/octet-stream");
        HtmlWeb._mimeTypes.Add(".arj", "application/arj");
        HtmlWeb._mimeTypes.Add(".arj", "application/octet-stream");
        HtmlWeb._mimeTypes.Add(".art", "image/x-jg");
        HtmlWeb._mimeTypes.Add(".asf", "video/x-ms-asf");
        HtmlWeb._mimeTypes.Add(".asm", "text/x-asm");
        HtmlWeb._mimeTypes.Add(".asp", "text/asp");
        HtmlWeb._mimeTypes.Add(".asx", "application/x-mplayer2");
        HtmlWeb._mimeTypes.Add(".asx", "video/x-ms-asf");
        HtmlWeb._mimeTypes.Add(".asx", "video/x-ms-asf-plugin");
        HtmlWeb._mimeTypes.Add(".au", "audio/basic");
        HtmlWeb._mimeTypes.Add(".au", "audio/x-au");
        HtmlWeb._mimeTypes.Add(".avi", "application/x-troff-msvideo");
        HtmlWeb._mimeTypes.Add(".avi", "video/avi");
        HtmlWeb._mimeTypes.Add(".avi", "video/msvideo");
        HtmlWeb._mimeTypes.Add(".avi", "video/x-msvideo");
        HtmlWeb._mimeTypes.Add(".avs", "video/avs-video");
        HtmlWeb._mimeTypes.Add(".bcpio", "application/x-bcpio");
        HtmlWeb._mimeTypes.Add(".bin", "application/mac-binary");
        HtmlWeb._mimeTypes.Add(".bin", "application/macbinary");
        HtmlWeb._mimeTypes.Add(".bin", "application/octet-stream");
        HtmlWeb._mimeTypes.Add(".bin", "application/x-binary");
        HtmlWeb._mimeTypes.Add(".bin", "application/x-macbinary");
        HtmlWeb._mimeTypes.Add(".bm", "image/bmp");
        HtmlWeb._mimeTypes.Add(".bmp", "image/bmp");
        HtmlWeb._mimeTypes.Add(".bmp", "image/x-windows-bmp");
        HtmlWeb._mimeTypes.Add(".boo", "application/book");
        HtmlWeb._mimeTypes.Add(".book", "application/book");
        HtmlWeb._mimeTypes.Add(".boz", "application/x-bzip2");
        HtmlWeb._mimeTypes.Add(".bsh", "application/x-bsh");
        HtmlWeb._mimeTypes.Add(".bz", "application/x-bzip");
        HtmlWeb._mimeTypes.Add(".bz2", "application/x-bzip2");
        HtmlWeb._mimeTypes.Add(".c", "text/plain");
        HtmlWeb._mimeTypes.Add(".c", "text/x-c");
        HtmlWeb._mimeTypes.Add(".c++", "text/plain");
        HtmlWeb._mimeTypes.Add(".cat", "application/vnd.ms-pki.seccat");
        HtmlWeb._mimeTypes.Add(".cc", "text/plain");
        HtmlWeb._mimeTypes.Add(".cc", "text/x-c");
        HtmlWeb._mimeTypes.Add(".ccad", "application/clariscad");
        HtmlWeb._mimeTypes.Add(".cco", "application/x-cocoa");
        HtmlWeb._mimeTypes.Add(".cdf", "application/cdf");
        HtmlWeb._mimeTypes.Add(".cdf", "application/x-cdf");
        HtmlWeb._mimeTypes.Add(".cdf", "application/x-netcdf");
        HtmlWeb._mimeTypes.Add(".cer", "application/pkix-cert");
        HtmlWeb._mimeTypes.Add(".cer", "application/x-x509-ca-cert");
        HtmlWeb._mimeTypes.Add(".cha", "application/x-chat");
        HtmlWeb._mimeTypes.Add(".chat", "application/x-chat");
        HtmlWeb._mimeTypes.Add(".class", "application/java");
        HtmlWeb._mimeTypes.Add(".class", "application/java-byte-code");
        HtmlWeb._mimeTypes.Add(".class", "application/x-java-class");
        HtmlWeb._mimeTypes.Add(".com", "application/octet-stream");
        HtmlWeb._mimeTypes.Add(".com", "text/plain");
        HtmlWeb._mimeTypes.Add(".conf", "text/plain");
        HtmlWeb._mimeTypes.Add(".cpio", "application/x-cpio");
        HtmlWeb._mimeTypes.Add(".cpp", "text/x-c");
        HtmlWeb._mimeTypes.Add(".cpt", "application/mac-compactpro");
        HtmlWeb._mimeTypes.Add(".cpt", "application/x-compactpro");
        HtmlWeb._mimeTypes.Add(".cpt", "application/x-cpt");
        HtmlWeb._mimeTypes.Add(".crl", "application/pkcs-crl");
        HtmlWeb._mimeTypes.Add(".crl", "application/pkix-crl");
        HtmlWeb._mimeTypes.Add(".crt", "application/pkix-cert");
        HtmlWeb._mimeTypes.Add(".crt", "application/x-x509-ca-cert");
        HtmlWeb._mimeTypes.Add(".crt", "application/x-x509-user-cert");
        HtmlWeb._mimeTypes.Add(".csh", "application/x-csh");
        HtmlWeb._mimeTypes.Add(".csh", "text/x-script.csh");
        HtmlWeb._mimeTypes.Add(".css", "application/x-pointplus");
        HtmlWeb._mimeTypes.Add(".css", "text/css");
        HtmlWeb._mimeTypes.Add(".cxx", "text/plain");
        HtmlWeb._mimeTypes.Add(".dcr", "application/x-director");
        HtmlWeb._mimeTypes.Add(".deepv", "application/x-deepv");
        HtmlWeb._mimeTypes.Add(".def", "text/plain");
        HtmlWeb._mimeTypes.Add(".der", "application/x-x509-ca-cert");
        HtmlWeb._mimeTypes.Add(".dif", "video/x-dv");
        HtmlWeb._mimeTypes.Add(".dir", "application/x-director");
        HtmlWeb._mimeTypes.Add(".dl", "video/dl");
        HtmlWeb._mimeTypes.Add(".dl", "video/x-dl");
        HtmlWeb._mimeTypes.Add(".doc", "application/msword");
        HtmlWeb._mimeTypes.Add(".dot", "application/msword");
        HtmlWeb._mimeTypes.Add(".dp", "application/commonground");
        HtmlWeb._mimeTypes.Add(".drw", "application/drafting");
        HtmlWeb._mimeTypes.Add(".dump", "application/octet-stream");
        HtmlWeb._mimeTypes.Add(".dv", "video/x-dv");
        HtmlWeb._mimeTypes.Add(".dvi", "application/x-dvi");
        HtmlWeb._mimeTypes.Add(".dwf", "model/vnd.dwf");
        HtmlWeb._mimeTypes.Add(".dwg", "application/acad");
        HtmlWeb._mimeTypes.Add(".dwg", "image/vnd.dwg");
        HtmlWeb._mimeTypes.Add(".dwg", "image/x-dwg");
        HtmlWeb._mimeTypes.Add(".dxf", "application/dxf");
        HtmlWeb._mimeTypes.Add(".dxf", "image/vnd.dwg");
        HtmlWeb._mimeTypes.Add(".dxf", "image/x-dwg");
        HtmlWeb._mimeTypes.Add(".dxr", "application/x-director");
        HtmlWeb._mimeTypes.Add(".el", "text/x-script.elisp");
        HtmlWeb._mimeTypes.Add(".elc", "application/x-bytecode.elisp");
        HtmlWeb._mimeTypes.Add(".elc", "application/x-elc");
        HtmlWeb._mimeTypes.Add(".env", "application/x-envoy");
        HtmlWeb._mimeTypes.Add(".eps", "application/postscript");
        HtmlWeb._mimeTypes.Add(".es", "application/x-esrehber");
        HtmlWeb._mimeTypes.Add(".etx", "text/x-setext");
        HtmlWeb._mimeTypes.Add(".evy", "application/envoy");
        HtmlWeb._mimeTypes.Add(".evy", "application/x-envoy");
        HtmlWeb._mimeTypes.Add(".exe", "application/octet-stream");
        HtmlWeb._mimeTypes.Add(".f", "text/plain");
        HtmlWeb._mimeTypes.Add(".f", "text/x-fortran");
        HtmlWeb._mimeTypes.Add(".f77", "text/x-fortran");
        HtmlWeb._mimeTypes.Add(".f90", "text/plain");
        HtmlWeb._mimeTypes.Add(".f90", "text/x-fortran");
        HtmlWeb._mimeTypes.Add(".fdf", "application/vnd.fdf");
        HtmlWeb._mimeTypes.Add(".fif", "application/fractals");
        HtmlWeb._mimeTypes.Add(".fif", "image/fif");
        HtmlWeb._mimeTypes.Add(".fli", "video/fli");
        HtmlWeb._mimeTypes.Add(".fli", "video/x-fli");
        HtmlWeb._mimeTypes.Add(".flo", "image/florian");
        HtmlWeb._mimeTypes.Add(".flx", "text/vnd.fmi.flexstor");
        HtmlWeb._mimeTypes.Add(".fmf", "video/x-atomic3d-feature");
        HtmlWeb._mimeTypes.Add(".for", "text/plain");
        HtmlWeb._mimeTypes.Add(".for", "text/x-fortran");
        HtmlWeb._mimeTypes.Add(".fpx", "image/vnd.fpx");
        HtmlWeb._mimeTypes.Add(".fpx", "image/vnd.net-fpx");
        HtmlWeb._mimeTypes.Add(".frl", "application/freeloader");
        HtmlWeb._mimeTypes.Add(".funk", "audio/make");
        HtmlWeb._mimeTypes.Add(".g", "text/plain");
        HtmlWeb._mimeTypes.Add(".g3", "image/g3fax");
        HtmlWeb._mimeTypes.Add(".gif", "image/gif");
        HtmlWeb._mimeTypes.Add(".gl", "video/gl");
        HtmlWeb._mimeTypes.Add(".gl", "video/x-gl");
        HtmlWeb._mimeTypes.Add(".gsd", "audio/x-gsm");
        HtmlWeb._mimeTypes.Add(".gsm", "audio/x-gsm");
        HtmlWeb._mimeTypes.Add(".gsp", "application/x-gsp");
        HtmlWeb._mimeTypes.Add(".gss", "application/x-gss");
        HtmlWeb._mimeTypes.Add(".gtar", "application/x-gtar");
        HtmlWeb._mimeTypes.Add(".gz", "application/x-compressed");
        HtmlWeb._mimeTypes.Add(".gz", "application/x-gzip");
        HtmlWeb._mimeTypes.Add(".gzip", "application/x-gzip");
        HtmlWeb._mimeTypes.Add(".gzip", "multipart/x-gzip");
        HtmlWeb._mimeTypes.Add(".h", "text/plain");
        HtmlWeb._mimeTypes.Add(".h", "text/x-h");
        HtmlWeb._mimeTypes.Add(".hdf", "application/x-hdf");
        HtmlWeb._mimeTypes.Add(".help", "application/x-helpfile");
        HtmlWeb._mimeTypes.Add(".hgl", "application/vnd.hp-hpgl");
        HtmlWeb._mimeTypes.Add(".hh", "text/plain");
        HtmlWeb._mimeTypes.Add(".hh", "text/x-h");
        HtmlWeb._mimeTypes.Add(".hlb", "text/x-script");
        HtmlWeb._mimeTypes.Add(".hlp", "application/hlp");
        HtmlWeb._mimeTypes.Add(".hlp", "application/x-helpfile");
        HtmlWeb._mimeTypes.Add(".hlp", "application/x-winhelp");
        HtmlWeb._mimeTypes.Add(".hpg", "application/vnd.hp-hpgl");
        HtmlWeb._mimeTypes.Add(".hpgl", "application/vnd.hp-hpgl");
        HtmlWeb._mimeTypes.Add(".hqx", "application/binhex");
        HtmlWeb._mimeTypes.Add(".hqx", "application/binhex4");
        HtmlWeb._mimeTypes.Add(".hqx", "application/mac-binhex");
        HtmlWeb._mimeTypes.Add(".hqx", "application/mac-binhex40");
        HtmlWeb._mimeTypes.Add(".hqx", "application/x-binhex40");
        HtmlWeb._mimeTypes.Add(".hqx", "application/x-mac-binhex40");
        HtmlWeb._mimeTypes.Add(".hta", "application/hta");
        HtmlWeb._mimeTypes.Add(".htc", "text/x-component");
        HtmlWeb._mimeTypes.Add(".htm", "text/html");
        HtmlWeb._mimeTypes.Add(".html", "text/html");
        HtmlWeb._mimeTypes.Add(".htmls", "text/html");
        HtmlWeb._mimeTypes.Add(".htt", "text/webviewhtml");
        HtmlWeb._mimeTypes.Add(".htx", "text/html");
        HtmlWeb._mimeTypes.Add(".ice", "x-conference/x-cooltalk");
        HtmlWeb._mimeTypes.Add(".ico", "image/x-icon");
        HtmlWeb._mimeTypes.Add(".idc", "text/plain");
        HtmlWeb._mimeTypes.Add(".ief", "image/ief");
        HtmlWeb._mimeTypes.Add(".iefs", "image/ief");
        HtmlWeb._mimeTypes.Add(".iges", "application/iges");
        HtmlWeb._mimeTypes.Add(".iges", "model/iges");
        HtmlWeb._mimeTypes.Add(".igs", "application/iges");
        HtmlWeb._mimeTypes.Add(".igs", "model/iges");
        HtmlWeb._mimeTypes.Add(".ima", "application/x-ima");
        HtmlWeb._mimeTypes.Add(".imap", "application/x-httpd-imap");
        HtmlWeb._mimeTypes.Add(".inf", "application/inf");
        HtmlWeb._mimeTypes.Add(".ins", "application/x-internett-signup");
        HtmlWeb._mimeTypes.Add(".ip", "application/x-ip2");
        HtmlWeb._mimeTypes.Add(".isu", "video/x-isvideo");
        HtmlWeb._mimeTypes.Add(".it", "audio/it");
        HtmlWeb._mimeTypes.Add(".iv", "application/x-inventor");
        HtmlWeb._mimeTypes.Add(".ivr", "i-world/i-vrml");
        HtmlWeb._mimeTypes.Add(".ivy", "application/x-livescreen");
        HtmlWeb._mimeTypes.Add(".jam", "audio/x-jam");
        HtmlWeb._mimeTypes.Add(".jav", "text/plain");
        HtmlWeb._mimeTypes.Add(".jav", "text/x-java-source");
        HtmlWeb._mimeTypes.Add(".java", "text/plain");
        HtmlWeb._mimeTypes.Add(".java", "text/x-java-source");
        HtmlWeb._mimeTypes.Add(".jcm", "application/x-java-commerce");
        HtmlWeb._mimeTypes.Add(".jfif", "image/jpeg");
        HtmlWeb._mimeTypes.Add(".jfif", "image/pjpeg");
        HtmlWeb._mimeTypes.Add(".jfif-tbnl", "image/jpeg");
        HtmlWeb._mimeTypes.Add(".jpe", "image/jpeg");
        HtmlWeb._mimeTypes.Add(".jpe", "image/pjpeg");
        HtmlWeb._mimeTypes.Add(".jpeg", "image/jpeg");
        HtmlWeb._mimeTypes.Add(".jpeg", "image/pjpeg");
        HtmlWeb._mimeTypes.Add(".jpg", "image/jpeg");
        HtmlWeb._mimeTypes.Add(".jpg", "image/pjpeg");
        HtmlWeb._mimeTypes.Add(".jps", "image/x-jps");
        HtmlWeb._mimeTypes.Add(".js", "application/x-javascript");
        HtmlWeb._mimeTypes.Add(".js", "application/javascript");
        HtmlWeb._mimeTypes.Add(".js", "application/ecmascript");
        HtmlWeb._mimeTypes.Add(".js", "text/javascript");
        HtmlWeb._mimeTypes.Add(".js", "text/ecmascript");
        HtmlWeb._mimeTypes.Add(".jut", "image/jutvision");
        HtmlWeb._mimeTypes.Add(".kar", "audio/midi");
        HtmlWeb._mimeTypes.Add(".kar", "music/x-karaoke");
        HtmlWeb._mimeTypes.Add(".ksh", "application/x-ksh");
        HtmlWeb._mimeTypes.Add(".ksh", "text/x-script.ksh");
        HtmlWeb._mimeTypes.Add(".la", "audio/nspaudio");
        HtmlWeb._mimeTypes.Add(".la", "audio/x-nspaudio");
        HtmlWeb._mimeTypes.Add(".lam", "audio/x-liveaudio");
        HtmlWeb._mimeTypes.Add(".latex", "application/x-latex");
        HtmlWeb._mimeTypes.Add(".lha", "application/lha");
        HtmlWeb._mimeTypes.Add(".lha", "application/octet-stream");
        HtmlWeb._mimeTypes.Add(".lha", "application/x-lha");
        HtmlWeb._mimeTypes.Add(".lhx", "application/octet-stream");
        HtmlWeb._mimeTypes.Add(".list", "text/plain");
        HtmlWeb._mimeTypes.Add(".lma", "audio/nspaudio");
        HtmlWeb._mimeTypes.Add(".lma", "audio/x-nspaudio");
        HtmlWeb._mimeTypes.Add(".log", "text/plain");
        HtmlWeb._mimeTypes.Add(".lsp", "application/x-lisp");
        HtmlWeb._mimeTypes.Add(".lsp", "text/x-script.lisp");
        HtmlWeb._mimeTypes.Add(".lst", "text/plain");
        HtmlWeb._mimeTypes.Add(".lsx", "text/x-la-asf");
        HtmlWeb._mimeTypes.Add(".ltx", "application/x-latex");
        HtmlWeb._mimeTypes.Add(".lzh", "application/octet-stream");
        HtmlWeb._mimeTypes.Add(".lzh", "application/x-lzh");
        HtmlWeb._mimeTypes.Add(".lzx", "application/lzx");
        HtmlWeb._mimeTypes.Add(".lzx", "application/octet-stream");
        HtmlWeb._mimeTypes.Add(".lzx", "application/x-lzx");
        HtmlWeb._mimeTypes.Add(".m", "text/plain");
        HtmlWeb._mimeTypes.Add(".m", "text/x-m");
        HtmlWeb._mimeTypes.Add(".m1v", "video/mpeg");
        HtmlWeb._mimeTypes.Add(".m2a", "audio/mpeg");
        HtmlWeb._mimeTypes.Add(".m2v", "video/mpeg");
        HtmlWeb._mimeTypes.Add(".m3u", "audio/x-mpequrl");
        HtmlWeb._mimeTypes.Add(".man", "application/x-troff-man");
        HtmlWeb._mimeTypes.Add(".map", "application/x-navimap");
        HtmlWeb._mimeTypes.Add(".mar", "text/plain");
        HtmlWeb._mimeTypes.Add(".mbd", "application/mbedlet");
        HtmlWeb._mimeTypes.Add(".mc$", "application/x-magic-cap-package-1.0");
        HtmlWeb._mimeTypes.Add(".mcd", "application/mcad");
        HtmlWeb._mimeTypes.Add(".mcd", "application/x-mathcad");
        HtmlWeb._mimeTypes.Add(".mcf", "image/vasa");
        HtmlWeb._mimeTypes.Add(".mcf", "text/mcf");
        HtmlWeb._mimeTypes.Add(".mcp", "application/netmc");
        HtmlWeb._mimeTypes.Add(".me", "application/x-troff-me");
        HtmlWeb._mimeTypes.Add(".mht", "message/rfc822");
        HtmlWeb._mimeTypes.Add(".mhtml", "message/rfc822");
        HtmlWeb._mimeTypes.Add(".mid", "application/x-midi");
        HtmlWeb._mimeTypes.Add(".mid", "audio/midi");
        HtmlWeb._mimeTypes.Add(".mid", "audio/x-mid");
        HtmlWeb._mimeTypes.Add(".mid", "audio/x-midi");
        HtmlWeb._mimeTypes.Add(".mid", "music/crescendo");
        HtmlWeb._mimeTypes.Add(".mid", "x-music/x-midi");
        HtmlWeb._mimeTypes.Add(".midi", "application/x-midi");
        HtmlWeb._mimeTypes.Add(".midi", "audio/midi");
        HtmlWeb._mimeTypes.Add(".midi", "audio/x-mid");
        HtmlWeb._mimeTypes.Add(".midi", "audio/x-midi");
        HtmlWeb._mimeTypes.Add(".midi", "music/crescendo");
        HtmlWeb._mimeTypes.Add(".midi", "x-music/x-midi");
        HtmlWeb._mimeTypes.Add(".mif", "application/x-frame");
        HtmlWeb._mimeTypes.Add(".mif", "application/x-mif");
        HtmlWeb._mimeTypes.Add(".mime", "message/rfc822");
        HtmlWeb._mimeTypes.Add(".mime", "www/mime");
        HtmlWeb._mimeTypes.Add(".mjf", "audio/x-vnd.audioexplosion.mjuicemediafile");
        HtmlWeb._mimeTypes.Add(".mjpg", "video/x-motion-jpeg");
        HtmlWeb._mimeTypes.Add(".mm", "application/base64");
        HtmlWeb._mimeTypes.Add(".mm", "application/x-meme");
        HtmlWeb._mimeTypes.Add(".mme", "application/base64");
        HtmlWeb._mimeTypes.Add(".mod", "audio/mod");
        HtmlWeb._mimeTypes.Add(".mod", "audio/x-mod");
        HtmlWeb._mimeTypes.Add(".moov", "video/quicktime");
        HtmlWeb._mimeTypes.Add(".mov", "video/quicktime");
        HtmlWeb._mimeTypes.Add(".movie", "video/x-sgi-movie");
        HtmlWeb._mimeTypes.Add(".mp2", "audio/mpeg");
        HtmlWeb._mimeTypes.Add(".mp2", "audio/x-mpeg");
        HtmlWeb._mimeTypes.Add(".mp2", "video/mpeg");
        HtmlWeb._mimeTypes.Add(".mp2", "video/x-mpeg");
        HtmlWeb._mimeTypes.Add(".mp2", "video/x-mpeq2a");
        HtmlWeb._mimeTypes.Add(".mp3", "audio/mpeg3");
        HtmlWeb._mimeTypes.Add(".mp3", "audio/x-mpeg-3");
        HtmlWeb._mimeTypes.Add(".mp3", "video/mpeg");
        HtmlWeb._mimeTypes.Add(".mp3", "video/x-mpeg");
        HtmlWeb._mimeTypes.Add(".mpa", "audio/mpeg");
        HtmlWeb._mimeTypes.Add(".mpa", "video/mpeg");
        HtmlWeb._mimeTypes.Add(".mpc", "application/x-project");
        HtmlWeb._mimeTypes.Add(".mpe", "video/mpeg");
        HtmlWeb._mimeTypes.Add(".mpeg", "video/mpeg");
        HtmlWeb._mimeTypes.Add(".mpg", "audio/mpeg");
        HtmlWeb._mimeTypes.Add(".mpg", "video/mpeg");
        HtmlWeb._mimeTypes.Add(".mpga", "audio/mpeg");
        HtmlWeb._mimeTypes.Add(".mpp", "application/vnd.ms-project");
        HtmlWeb._mimeTypes.Add(".mpt", "application/x-project");
        HtmlWeb._mimeTypes.Add(".mpv", "application/x-project");
        HtmlWeb._mimeTypes.Add(".mpx", "application/x-project");
        HtmlWeb._mimeTypes.Add(".mrc", "application/marc");
        HtmlWeb._mimeTypes.Add(".ms", "application/x-troff-ms");
        HtmlWeb._mimeTypes.Add(".mv", "video/x-sgi-movie");
        HtmlWeb._mimeTypes.Add(".my", "audio/make");
        HtmlWeb._mimeTypes.Add(".mzz", "application/x-vnd.audioexplosion.mzz");
        HtmlWeb._mimeTypes.Add(".nap", "image/naplps");
        HtmlWeb._mimeTypes.Add(".naplps", "image/naplps");
        HtmlWeb._mimeTypes.Add(".nc", "application/x-netcdf");
        HtmlWeb._mimeTypes.Add(".ncm", "application/vnd.nokia.configuration-message");
        HtmlWeb._mimeTypes.Add(".nif", "image/x-niff");
        HtmlWeb._mimeTypes.Add(".niff", "image/x-niff");
        HtmlWeb._mimeTypes.Add(".nix", "application/x-mix-transfer");
        HtmlWeb._mimeTypes.Add(".nsc", "application/x-conference");
        HtmlWeb._mimeTypes.Add(".nvd", "application/x-navidoc");
        HtmlWeb._mimeTypes.Add(".o", "application/octet-stream");
        HtmlWeb._mimeTypes.Add(".oda", "application/oda");
        HtmlWeb._mimeTypes.Add(".omc", "application/x-omc");
        HtmlWeb._mimeTypes.Add(".omcd", "application/x-omcdatamaker");
        HtmlWeb._mimeTypes.Add(".omcr", "application/x-omcregerator");
        HtmlWeb._mimeTypes.Add(".p", "text/x-pascal");
        HtmlWeb._mimeTypes.Add(".p10", "application/pkcs10");
        HtmlWeb._mimeTypes.Add(".p10", "application/x-pkcs10");
        HtmlWeb._mimeTypes.Add(".p12", "application/pkcs-12");
        HtmlWeb._mimeTypes.Add(".p12", "application/x-pkcs12");
        HtmlWeb._mimeTypes.Add(".p7a", "application/x-pkcs7-signature");
        HtmlWeb._mimeTypes.Add(".p7c", "application/pkcs7-mime");
        HtmlWeb._mimeTypes.Add(".p7c", "application/x-pkcs7-mime");
        HtmlWeb._mimeTypes.Add(".p7m", "application/pkcs7-mime");
        HtmlWeb._mimeTypes.Add(".p7m", "application/x-pkcs7-mime");
        HtmlWeb._mimeTypes.Add(".p7r", "application/x-pkcs7-certreqresp");
        HtmlWeb._mimeTypes.Add(".p7s", "application/pkcs7-signature");
        HtmlWeb._mimeTypes.Add(".part", "application/pro_eng");
        HtmlWeb._mimeTypes.Add(".pas", "text/pascal");
        HtmlWeb._mimeTypes.Add(".pbm", "image/x-portable-bitmap");
        HtmlWeb._mimeTypes.Add(".pcl", "application/vnd.hp-pcl");
        HtmlWeb._mimeTypes.Add(".pcl", "application/x-pcl");
        HtmlWeb._mimeTypes.Add(".pct", "image/x-pict");
        HtmlWeb._mimeTypes.Add(".pcx", "image/x-pcx");
        HtmlWeb._mimeTypes.Add(".pdb", "chemical/x-pdb");
        HtmlWeb._mimeTypes.Add(".pdf", "application/pdf");
        HtmlWeb._mimeTypes.Add(".pfunk", "audio/make");
        HtmlWeb._mimeTypes.Add(".pfunk", "audio/make.my.funk");
        HtmlWeb._mimeTypes.Add(".pgm", "image/x-portable-graymap");
        HtmlWeb._mimeTypes.Add(".pgm", "image/x-portable-greymap");
        HtmlWeb._mimeTypes.Add(".pic", "image/pict");
        HtmlWeb._mimeTypes.Add(".pict", "image/pict");
        HtmlWeb._mimeTypes.Add(".pkg", "application/x-newton-compatible-pkg");
        HtmlWeb._mimeTypes.Add(".pko", "application/vnd.ms-pki.pko");
        HtmlWeb._mimeTypes.Add(".pl", "text/plain");
        HtmlWeb._mimeTypes.Add(".pl", "text/x-script.perl");
        HtmlWeb._mimeTypes.Add(".plx", "application/x-pixclscript");
        HtmlWeb._mimeTypes.Add(".pm", "image/x-xpixmap");
        HtmlWeb._mimeTypes.Add(".pm", "text/x-script.perl-module");
        HtmlWeb._mimeTypes.Add(".pm4", "application/x-pagemaker");
        HtmlWeb._mimeTypes.Add(".pm5", "application/x-pagemaker");
        HtmlWeb._mimeTypes.Add(".png", "image/png");
        HtmlWeb._mimeTypes.Add(".pnm", "application/x-portable-anymap");
        HtmlWeb._mimeTypes.Add(".pnm", "image/x-portable-anymap");
        HtmlWeb._mimeTypes.Add(".pot", "application/mspowerpoint");
        HtmlWeb._mimeTypes.Add(".pot", "application/vnd.ms-powerpoint");
        HtmlWeb._mimeTypes.Add(".pov", "model/x-pov");
        HtmlWeb._mimeTypes.Add(".ppa", "application/vnd.ms-powerpoint");
        HtmlWeb._mimeTypes.Add(".ppm", "image/x-portable-pixmap");
        HtmlWeb._mimeTypes.Add(".pps", "application/mspowerpoint");
        HtmlWeb._mimeTypes.Add(".pps", "application/vnd.ms-powerpoint");
        HtmlWeb._mimeTypes.Add(".ppt", "application/mspowerpoint");
        HtmlWeb._mimeTypes.Add(".ppt", "application/powerpoint");
        HtmlWeb._mimeTypes.Add(".ppt", "application/vnd.ms-powerpoint");
        HtmlWeb._mimeTypes.Add(".ppt", "application/x-mspowerpoint");
        HtmlWeb._mimeTypes.Add(".ppz", "application/mspowerpoint");
        HtmlWeb._mimeTypes.Add(".pre", "application/x-freelance");
        HtmlWeb._mimeTypes.Add(".prt", "application/pro_eng");
        HtmlWeb._mimeTypes.Add(".ps", "application/postscript");
        HtmlWeb._mimeTypes.Add(".psd", "application/octet-stream");
        HtmlWeb._mimeTypes.Add(".pvu", "paleovu/x-pv");
        HtmlWeb._mimeTypes.Add(".pwz", "application/vnd.ms-powerpoint");
        HtmlWeb._mimeTypes.Add(".py", "text/x-script.phyton");
        HtmlWeb._mimeTypes.Add(".pyc", "applicaiton/x-bytecode.python");
        HtmlWeb._mimeTypes.Add(".qcp", "audio/vnd.qcelp");
        HtmlWeb._mimeTypes.Add(".qd3", "x-world/x-3dmf");
        HtmlWeb._mimeTypes.Add(".qd3d", "x-world/x-3dmf");
        HtmlWeb._mimeTypes.Add(".qif", "image/x-quicktime");
        HtmlWeb._mimeTypes.Add(".qt", "video/quicktime");
        HtmlWeb._mimeTypes.Add(".qtc", "video/x-qtc");
        HtmlWeb._mimeTypes.Add(".qti", "image/x-quicktime");
        HtmlWeb._mimeTypes.Add(".qtif", "image/x-quicktime");
        HtmlWeb._mimeTypes.Add(".ra", "audio/x-pn-realaudio");
        HtmlWeb._mimeTypes.Add(".ra", "audio/x-pn-realaudio-plugin");
        HtmlWeb._mimeTypes.Add(".ra", "audio/x-realaudio");
        HtmlWeb._mimeTypes.Add(".ram", "audio/x-pn-realaudio");
        HtmlWeb._mimeTypes.Add(".ras", "application/x-cmu-raster");
        HtmlWeb._mimeTypes.Add(".ras", "image/cmu-raster");
        HtmlWeb._mimeTypes.Add(".ras", "image/x-cmu-raster");
        HtmlWeb._mimeTypes.Add(".rast", "image/cmu-raster");
        HtmlWeb._mimeTypes.Add(".rexx", "text/x-script.rexx");
        HtmlWeb._mimeTypes.Add(".rf", "image/vnd.rn-realflash");
        HtmlWeb._mimeTypes.Add(".rgb", "image/x-rgb");
        HtmlWeb._mimeTypes.Add(".rm", "application/vnd.rn-realmedia");
        HtmlWeb._mimeTypes.Add(".rm", "audio/x-pn-realaudio");
        HtmlWeb._mimeTypes.Add(".rmi", "audio/mid");
        HtmlWeb._mimeTypes.Add(".rmm", "audio/x-pn-realaudio");
        HtmlWeb._mimeTypes.Add(".rmp", "audio/x-pn-realaudio");
        HtmlWeb._mimeTypes.Add(".rmp", "audio/x-pn-realaudio-plugin");
        HtmlWeb._mimeTypes.Add(".rng", "application/ringing-tones");
        HtmlWeb._mimeTypes.Add(".rng", "application/vnd.nokia.ringing-tone");
        HtmlWeb._mimeTypes.Add(".rnx", "application/vnd.rn-realplayer");
        HtmlWeb._mimeTypes.Add(".roff", "application/x-troff");
        HtmlWeb._mimeTypes.Add(".rp", "image/vnd.rn-realpix");
        HtmlWeb._mimeTypes.Add(".rpm", "audio/x-pn-realaudio-plugin");
        HtmlWeb._mimeTypes.Add(".rt", "text/richtext");
        HtmlWeb._mimeTypes.Add(".rt", "text/vnd.rn-realtext");
        HtmlWeb._mimeTypes.Add(".rtf", "application/rtf");
        HtmlWeb._mimeTypes.Add(".rtf", "application/x-rtf");
        HtmlWeb._mimeTypes.Add(".rtf", "text/richtext");
        HtmlWeb._mimeTypes.Add(".rtx", "application/rtf");
        HtmlWeb._mimeTypes.Add(".rtx", "text/richtext");
        HtmlWeb._mimeTypes.Add(".rv", "video/vnd.rn-realvideo");
        HtmlWeb._mimeTypes.Add(".s", "text/x-asm");
        HtmlWeb._mimeTypes.Add(".s3m", "audio/s3m");
        HtmlWeb._mimeTypes.Add(".saveme", "application/octet-stream");
        HtmlWeb._mimeTypes.Add(".sbk", "application/x-tbook");
        HtmlWeb._mimeTypes.Add(".scm", "application/x-lotusscreencam");
        HtmlWeb._mimeTypes.Add(".scm", "text/x-script.guile");
        HtmlWeb._mimeTypes.Add(".scm", "text/x-script.scheme");
        HtmlWeb._mimeTypes.Add(".scm", "video/x-scm");
        HtmlWeb._mimeTypes.Add(".sdml", "text/plain");
        HtmlWeb._mimeTypes.Add(".sdp", "application/sdp");
        HtmlWeb._mimeTypes.Add(".sdp", "application/x-sdp");
        HtmlWeb._mimeTypes.Add(".sdr", "application/sounder");
        HtmlWeb._mimeTypes.Add(".sea", "application/sea");
        HtmlWeb._mimeTypes.Add(".sea", "application/x-sea");
        HtmlWeb._mimeTypes.Add(".set", "application/set");
        HtmlWeb._mimeTypes.Add(".sgm", "text/sgml");
        HtmlWeb._mimeTypes.Add(".sgm", "text/x-sgml");
        HtmlWeb._mimeTypes.Add(".sgml", "text/sgml");
        HtmlWeb._mimeTypes.Add(".sgml", "text/x-sgml");
        HtmlWeb._mimeTypes.Add(".sh", "application/x-bsh");
        HtmlWeb._mimeTypes.Add(".sh", "application/x-sh");
        HtmlWeb._mimeTypes.Add(".sh", "application/x-shar");
        HtmlWeb._mimeTypes.Add(".sh", "text/x-script.sh");
        HtmlWeb._mimeTypes.Add(".shar", "application/x-bsh");
        HtmlWeb._mimeTypes.Add(".shar", "application/x-shar");
        HtmlWeb._mimeTypes.Add(".shtml", "text/html");
        HtmlWeb._mimeTypes.Add(".shtml", "text/x-server-parsed-html");
        HtmlWeb._mimeTypes.Add(".sid", "audio/x-psid");
        HtmlWeb._mimeTypes.Add(".sit", "application/x-sit");
        HtmlWeb._mimeTypes.Add(".sit", "application/x-stuffit");
        HtmlWeb._mimeTypes.Add(".skd", "application/x-koan");
        HtmlWeb._mimeTypes.Add(".skm", "application/x-koan");
        HtmlWeb._mimeTypes.Add(".skp", "application/x-koan");
        HtmlWeb._mimeTypes.Add(".skt", "application/x-koan");
        HtmlWeb._mimeTypes.Add(".sl", "application/x-seelogo");
        HtmlWeb._mimeTypes.Add(".smi", "application/smil");
        HtmlWeb._mimeTypes.Add(".smil", "application/smil");
        HtmlWeb._mimeTypes.Add(".snd", "audio/basic");
        HtmlWeb._mimeTypes.Add(".snd", "audio/x-adpcm");
        HtmlWeb._mimeTypes.Add(".sol", "application/solids");
        HtmlWeb._mimeTypes.Add(".spc", "application/x-pkcs7-certificates");
        HtmlWeb._mimeTypes.Add(".spc", "text/x-speech");
        HtmlWeb._mimeTypes.Add(".spl", "application/futuresplash");
        HtmlWeb._mimeTypes.Add(".spr", "application/x-sprite");
        HtmlWeb._mimeTypes.Add(".sprite", "application/x-sprite");
        HtmlWeb._mimeTypes.Add(".src", "application/x-wais-source");
        HtmlWeb._mimeTypes.Add(".ssi", "text/x-server-parsed-html");
        HtmlWeb._mimeTypes.Add(".ssm", "application/streamingmedia");
        HtmlWeb._mimeTypes.Add(".sst", "application/vnd.ms-pki.certstore");
        HtmlWeb._mimeTypes.Add(".step", "application/step");
        HtmlWeb._mimeTypes.Add(".stl", "application/sla");
        HtmlWeb._mimeTypes.Add(".stl", "application/vnd.ms-pki.stl");
        HtmlWeb._mimeTypes.Add(".stl", "application/x-navistyle");
        HtmlWeb._mimeTypes.Add(".stp", "application/step");
        HtmlWeb._mimeTypes.Add(".sv4cpio", "application/x-sv4cpio");
        HtmlWeb._mimeTypes.Add(".sv4crc", "application/x-sv4crc");
        HtmlWeb._mimeTypes.Add(".svf", "image/vnd.dwg");
        HtmlWeb._mimeTypes.Add(".svf", "image/x-dwg");
        HtmlWeb._mimeTypes.Add(".svr", "application/x-world");
        HtmlWeb._mimeTypes.Add(".svr", "x-world/x-svr");
        HtmlWeb._mimeTypes.Add(".swf", "application/x-shockwave-flash");
        HtmlWeb._mimeTypes.Add(".t", "application/x-troff");
        HtmlWeb._mimeTypes.Add(".talk", "text/x-speech");
        HtmlWeb._mimeTypes.Add(".tar", "application/x-tar");
        HtmlWeb._mimeTypes.Add(".tbk", "application/toolbook");
        HtmlWeb._mimeTypes.Add(".tbk", "application/x-tbook");
        HtmlWeb._mimeTypes.Add(".tcl", "application/x-tcl");
        HtmlWeb._mimeTypes.Add(".tcl", "text/x-script.tcl");
        HtmlWeb._mimeTypes.Add(".tcsh", "text/x-script.tcsh");
        HtmlWeb._mimeTypes.Add(".tex", "application/x-tex");
        HtmlWeb._mimeTypes.Add(".texi", "application/x-texinfo");
        HtmlWeb._mimeTypes.Add(".texinfo", "application/x-texinfo");
        HtmlWeb._mimeTypes.Add(".text", "application/plain");
        HtmlWeb._mimeTypes.Add(".text", "text/plain");
        HtmlWeb._mimeTypes.Add(".tgz", "application/gnutar");
        HtmlWeb._mimeTypes.Add(".tgz", "application/x-compressed");
        HtmlWeb._mimeTypes.Add(".tif", "image/tiff");
        HtmlWeb._mimeTypes.Add(".tif", "image/x-tiff");
        HtmlWeb._mimeTypes.Add(".tiff", "image/tiff");
        HtmlWeb._mimeTypes.Add(".tiff", "image/x-tiff");
        HtmlWeb._mimeTypes.Add(".tr", "application/x-troff");
        HtmlWeb._mimeTypes.Add(".tsi", "audio/tsp-audio");
        HtmlWeb._mimeTypes.Add(".tsp", "application/dsptype");
        HtmlWeb._mimeTypes.Add(".tsp", "audio/tsplayer");
        HtmlWeb._mimeTypes.Add(".tsv", "text/tab-separated-values");
        HtmlWeb._mimeTypes.Add(".turbot", "image/florian");
        HtmlWeb._mimeTypes.Add(".txt", "text/plain");
        HtmlWeb._mimeTypes.Add(".uil", "text/x-uil");
        HtmlWeb._mimeTypes.Add(".uni", "text/uri-list");
        HtmlWeb._mimeTypes.Add(".unis", "text/uri-list");
        HtmlWeb._mimeTypes.Add(".unv", "application/i-deas");
        HtmlWeb._mimeTypes.Add(".uri", "text/uri-list");
        HtmlWeb._mimeTypes.Add(".uris", "text/uri-list");
        HtmlWeb._mimeTypes.Add(".ustar", "application/x-ustar");
        HtmlWeb._mimeTypes.Add(".ustar", "multipart/x-ustar");
        HtmlWeb._mimeTypes.Add(".uu", "application/octet-stream");
        HtmlWeb._mimeTypes.Add(".uu", "text/x-uuencode");
        HtmlWeb._mimeTypes.Add(".uue", "text/x-uuencode");
        HtmlWeb._mimeTypes.Add(".vcd", "application/x-cdlink");
        HtmlWeb._mimeTypes.Add(".vcs", "text/x-vcalendar");
        HtmlWeb._mimeTypes.Add(".vda", "application/vda");
        HtmlWeb._mimeTypes.Add(".vdo", "video/vdo");
        HtmlWeb._mimeTypes.Add(".vew", "application/groupwise");
        HtmlWeb._mimeTypes.Add(".viv", "video/vivo");
        HtmlWeb._mimeTypes.Add(".viv", "video/vnd.vivo");
        HtmlWeb._mimeTypes.Add(".vivo", "video/vivo");
        HtmlWeb._mimeTypes.Add(".vivo", "video/vnd.vivo");
        HtmlWeb._mimeTypes.Add(".vmd", "application/vocaltec-media-desc");
        HtmlWeb._mimeTypes.Add(".vmf", "application/vocaltec-media-file");
        HtmlWeb._mimeTypes.Add(".voc", "audio/voc");
        HtmlWeb._mimeTypes.Add(".voc", "audio/x-voc");
        HtmlWeb._mimeTypes.Add(".vos", "video/vosaic");
        HtmlWeb._mimeTypes.Add(".vox", "audio/voxware");
        HtmlWeb._mimeTypes.Add(".vqe", "audio/x-twinvq-plugin");
        HtmlWeb._mimeTypes.Add(".vqf", "audio/x-twinvq");
        HtmlWeb._mimeTypes.Add(".vql", "audio/x-twinvq-plugin");
        HtmlWeb._mimeTypes.Add(".vrml", "application/x-vrml");
        HtmlWeb._mimeTypes.Add(".vrml", "model/vrml");
        HtmlWeb._mimeTypes.Add(".vrml", "x-world/x-vrml");
        HtmlWeb._mimeTypes.Add(".vrt", "x-world/x-vrt");
        HtmlWeb._mimeTypes.Add(".vsd", "application/x-visio");
        HtmlWeb._mimeTypes.Add(".vst", "application/x-visio");
        HtmlWeb._mimeTypes.Add(".vsw", "application/x-visio");
        HtmlWeb._mimeTypes.Add(".w60", "application/wordperfect6.0");
        HtmlWeb._mimeTypes.Add(".w61", "application/wordperfect6.1");
        HtmlWeb._mimeTypes.Add(".w6w", "application/msword");
        HtmlWeb._mimeTypes.Add(".wav", "audio/wav");
        HtmlWeb._mimeTypes.Add(".wav", "audio/x-wav");
        HtmlWeb._mimeTypes.Add(".wb1", "application/x-qpro");
        HtmlWeb._mimeTypes.Add(".wbmp", "image/vnd.wap.wbmp");
        HtmlWeb._mimeTypes.Add(".web", "application/vnd.xara");
        HtmlWeb._mimeTypes.Add(".wiz", "application/msword");
        HtmlWeb._mimeTypes.Add(".wk1", "application/x-123");
        HtmlWeb._mimeTypes.Add(".wmf", "windows/metafile");
        HtmlWeb._mimeTypes.Add(".wml", "text/vnd.wap.wml");
        HtmlWeb._mimeTypes.Add(".wmlc", "application/vnd.wap.wmlc");
        HtmlWeb._mimeTypes.Add(".wmls", "text/vnd.wap.wmlscript");
        HtmlWeb._mimeTypes.Add(".wmlsc", "application/vnd.wap.wmlscriptc");
        HtmlWeb._mimeTypes.Add(".word", "application/msword");
        HtmlWeb._mimeTypes.Add(".wp", "application/wordperfect");
        HtmlWeb._mimeTypes.Add(".wp5", "application/wordperfect");
        HtmlWeb._mimeTypes.Add(".wp5", "application/wordperfect6.0");
        HtmlWeb._mimeTypes.Add(".wp6", "application/wordperfect");
        HtmlWeb._mimeTypes.Add(".wpd", "application/wordperfect");
        HtmlWeb._mimeTypes.Add(".wpd", "application/x-wpwin");
        HtmlWeb._mimeTypes.Add(".wq1", "application/x-lotus");
        HtmlWeb._mimeTypes.Add(".wri", "application/mswrite");
        HtmlWeb._mimeTypes.Add(".wri", "application/x-wri");
        HtmlWeb._mimeTypes.Add(".wrl", "application/x-world");
        HtmlWeb._mimeTypes.Add(".wrl", "model/vrml");
        HtmlWeb._mimeTypes.Add(".wrl", "x-world/x-vrml");
        HtmlWeb._mimeTypes.Add(".wrz", "model/vrml");
        HtmlWeb._mimeTypes.Add(".wrz", "x-world/x-vrml");
        HtmlWeb._mimeTypes.Add(".wsc", "text/scriplet");
        HtmlWeb._mimeTypes.Add(".wsrc", "application/x-wais-source");
        HtmlWeb._mimeTypes.Add(".wtk", "application/x-wintalk");
        HtmlWeb._mimeTypes.Add(".xbm", "image/x-xbitmap");
        HtmlWeb._mimeTypes.Add(".xbm", "image/x-xbm");
        HtmlWeb._mimeTypes.Add(".xbm", "image/xbm");
        HtmlWeb._mimeTypes.Add(".xdr", "video/x-amt-demorun");
        HtmlWeb._mimeTypes.Add(".xgz", "xgl/drawing");
        HtmlWeb._mimeTypes.Add(".xif", "image/vnd.xiff");
        HtmlWeb._mimeTypes.Add(".xl", "application/excel");
        HtmlWeb._mimeTypes.Add(".xla", "application/excel");
        HtmlWeb._mimeTypes.Add(".xla", "application/x-excel");
        HtmlWeb._mimeTypes.Add(".xla", "application/x-msexcel");
        HtmlWeb._mimeTypes.Add(".xlb", "application/excel");
        HtmlWeb._mimeTypes.Add(".xlb", "application/vnd.ms-excel");
        HtmlWeb._mimeTypes.Add(".xlb", "application/x-excel");
        HtmlWeb._mimeTypes.Add(".xlc", "application/excel");
        HtmlWeb._mimeTypes.Add(".xlc", "application/vnd.ms-excel");
        HtmlWeb._mimeTypes.Add(".xlc", "application/x-excel");
        HtmlWeb._mimeTypes.Add(".xld", "application/excel");
        HtmlWeb._mimeTypes.Add(".xld", "application/x-excel");
        HtmlWeb._mimeTypes.Add(".xlk", "application/excel");
        HtmlWeb._mimeTypes.Add(".xlk", "application/x-excel");
        HtmlWeb._mimeTypes.Add(".xll", "application/excel");
        HtmlWeb._mimeTypes.Add(".xll", "application/vnd.ms-excel");
        HtmlWeb._mimeTypes.Add(".xll", "application/x-excel");
        HtmlWeb._mimeTypes.Add(".xlm", "application/excel");
        HtmlWeb._mimeTypes.Add(".xlm", "application/vnd.ms-excel");
        HtmlWeb._mimeTypes.Add(".xlm", "application/x-excel");
        HtmlWeb._mimeTypes.Add(".xls", "application/excel");
        HtmlWeb._mimeTypes.Add(".xls", "application/vnd.ms-excel");
        HtmlWeb._mimeTypes.Add(".xls", "application/x-excel");
        HtmlWeb._mimeTypes.Add(".xls", "application/x-msexcel");
        HtmlWeb._mimeTypes.Add(".xlt", "application/excel");
        HtmlWeb._mimeTypes.Add(".xlt", "application/x-excel");
        HtmlWeb._mimeTypes.Add(".xlv", "application/excel");
        HtmlWeb._mimeTypes.Add(".xlv", "application/x-excel");
        HtmlWeb._mimeTypes.Add(".xlw", "application/excel");
        HtmlWeb._mimeTypes.Add(".xlw", "application/vnd.ms-excel");
        HtmlWeb._mimeTypes.Add(".xlw", "application/x-excel");
        HtmlWeb._mimeTypes.Add(".xlw", "application/x-msexcel");
        HtmlWeb._mimeTypes.Add(".xm", "audio/xm");
        HtmlWeb._mimeTypes.Add(".xml", "application/xml");
        HtmlWeb._mimeTypes.Add(".xml", "text/xml");
        HtmlWeb._mimeTypes.Add(".xmz", "xgl/movie");
        HtmlWeb._mimeTypes.Add(".xpix", "application/x-vnd.ls-xpix");
        HtmlWeb._mimeTypes.Add(".xpm", "image/x-xpixmap");
        HtmlWeb._mimeTypes.Add(".xpm", "image/xpm");
        HtmlWeb._mimeTypes.Add(".x-png", "image/png");
        HtmlWeb._mimeTypes.Add(".xsr", "video/x-amt-showrun");
        HtmlWeb._mimeTypes.Add(".xwd", "image/x-xwd");
        HtmlWeb._mimeTypes.Add(".xwd", "image/x-xwindowdump");
        HtmlWeb._mimeTypes.Add(".xyz", "chemical/x-pdb");
        HtmlWeb._mimeTypes.Add(".z", "application/x-compress");
        HtmlWeb._mimeTypes.Add(".z", "application/x-compressed");
        HtmlWeb._mimeTypes.Add(".zip", "application/x-compressed");
        HtmlWeb._mimeTypes.Add(".zip", "application/x-zip-compressed");
        HtmlWeb._mimeTypes.Add(".zip", "application/zip");
        HtmlWeb._mimeTypes.Add(".zip", "multipart/x-zip");
        HtmlWeb._mimeTypes.Add(".zoo", "application/octet-stream");
        HtmlWeb._mimeTypes.Add(".zsh", "text/x-script.zsh");
        return HtmlWeb._mimeTypes;
      }
    }

    public bool AutoDetectEncoding
    {
      get
      {
        return this._autoDetectEncoding;
      }
      set
      {
        this._autoDetectEncoding = value;
      }
    }

    public Encoding OverrideEncoding
    {
      get
      {
        return this._encoding;
      }
      set
      {
        this._encoding = value;
      }
    }

    public bool CacheOnly
    {
      get
      {
        return this._cacheOnly;
      }
      set
      {
        if (value && !this.UsingCache)
          throw new HtmlWebException("Cache is not enabled. Set UsingCache to true first.");
        this._cacheOnly = value;
      }
    }

    public string CachePath
    {
      get
      {
        return this._cachePath;
      }
      set
      {
        this._cachePath = value;
      }
    }

    public bool FromCache
    {
      get
      {
        return this._fromCache;
      }
    }

    public int RequestDuration
    {
      get
      {
        return this._requestDuration;
      }
    }

    public Uri ResponseUri
    {
      get
      {
        return this._responseUri;
      }
    }

    public HttpStatusCode StatusCode
    {
      get
      {
        return this._statusCode;
      }
    }

    public int StreamBufferSize
    {
      get
      {
        return this._streamBufferSize;
      }
      set
      {
        if (this._streamBufferSize <= 0)
          throw new ArgumentException("Size must be greater than zero.");
        this._streamBufferSize = value;
      }
    }

    public bool UseCookies
    {
      get
      {
        return this._useCookies;
      }
      set
      {
        this._useCookies = value;
      }
    }

    public string UserAgent
    {
      get
      {
        return this._userAgent;
      }
      set
      {
        this._userAgent = value;
      }
    }

    public bool UsingCache
    {
      get
      {
        if (this._cachePath != null)
          return this._usingCache;
        return false;
      }
      set
      {
        if (value && this._cachePath == null)
          throw new HtmlWebException("You need to define a CachePath first.");
        this._usingCache = value;
      }
    }

    public static string GetContentTypeForExtension(string extension, string def)
    {
      PermissionHelper permissionHelper = new PermissionHelper();
      if (string.IsNullOrEmpty(extension))
        return def;
      string str = "";
      if (!permissionHelper.GetIsRegistryAvailable())
        str = !HtmlWeb.MimeTypes.ContainsKey(extension) ? def : HtmlWeb.MimeTypes[extension];
      if (!permissionHelper.GetIsDnsAvailable())
      {
        try
        {
          RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey(extension, false);
          if (registryKey != null)
            str = (string) registryKey.GetValue("", (object) def);
        }
        catch (Exception ex)
        {
          str = def;
        }
      }
      return str;
    }

    public static string GetExtensionForContentType(string contentType, string def)
    {
      PermissionHelper permissionHelper = new PermissionHelper();
      if (string.IsNullOrEmpty(contentType))
        return def;
      string str = "";
      if (!permissionHelper.GetIsRegistryAvailable())
      {
        if (HtmlWeb.MimeTypes.ContainsValue(contentType))
        {
          foreach (KeyValuePair<string, string> mimeType in HtmlWeb.MimeTypes)
          {
            if (mimeType.Value == contentType)
              return mimeType.Value;
          }
        }
        return def;
      }
      if (permissionHelper.GetIsRegistryAvailable())
      {
        try
        {
          RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey("MIME\\Database\\Content Type\\" + contentType, false);
          if (registryKey != null)
            str = (string) registryKey.GetValue("Extension", (object) def);
        }
        catch (Exception ex)
        {
          str = def;
        }
      }
      return str;
    }

    public object CreateInstance(string url, Type type)
    {
      return this.CreateInstance(url, (string) null, (XsltArgumentList) null, type);
    }

    public void Get(string url, string path)
    {
      this.Get(url, path, "GET");
    }

    public void Get(string url, string path, WebProxy proxy, NetworkCredential credentials)
    {
      this.Get(url, path, proxy, credentials, "GET");
    }

    public void Get(string url, string path, string method)
    {
      Uri uri = new Uri(url);
      if (!(uri.Scheme == Uri.UriSchemeHttps) && !(uri.Scheme == Uri.UriSchemeHttp))
        throw new HtmlWebException("Unsupported uri scheme: '" + uri.Scheme + "'.");
      int num = (int) this.Get(uri, method, path, (HtmlDocument) null, (IWebProxy) null, (ICredentials) null);
    }

    public void Get(string url, string path, WebProxy proxy, NetworkCredential credentials, string method)
    {
      Uri uri = new Uri(url);
      if (!(uri.Scheme == Uri.UriSchemeHttps) && !(uri.Scheme == Uri.UriSchemeHttp))
        throw new HtmlWebException("Unsupported uri scheme: '" + uri.Scheme + "'.");
      int num = (int) this.Get(uri, method, path, (HtmlDocument) null, (IWebProxy) proxy, (ICredentials) credentials);
    }

    public string GetCachePath(Uri uri)
    {
      if (uri == (Uri) null)
        throw new ArgumentNullException(nameof (uri));
      if (!this.UsingCache)
        throw new HtmlWebException("Cache is not enabled. Set UsingCache to true first.");
      return !(uri.AbsolutePath == "/") ? Path.Combine(this._cachePath, (uri.Host + uri.AbsolutePath).Replace('/', '\\')) : Path.Combine(this._cachePath, ".htm");
    }

    public HtmlDocument Load(string url)
    {
      return this.Load(url, "GET");
    }

    public HtmlDocument Load(string url, string proxyHost, int proxyPort, string userId, string password)
    {
      WebProxy proxy = new WebProxy(proxyHost, proxyPort);
      proxy.BypassProxyOnLocal = true;
      NetworkCredential credentials = (NetworkCredential) null;
      if (userId != null && password != null)
      {
        credentials = new NetworkCredential(userId, password);
        CredentialCache credentialCache = new CredentialCache()
        {
          {
            proxy.Address,
            "Basic",
            credentials
          },
          {
            proxy.Address,
            "Digest",
            credentials
          }
        };
      }
      return this.Load(url, "GET", proxy, credentials);
    }

    public HtmlDocument Load(string url, string method)
    {
      Uri uri = new Uri(url);
      HtmlDocument document;
      if (uri.Scheme == Uri.UriSchemeHttps || uri.Scheme == Uri.UriSchemeHttp)
      {
        document = this.LoadUrl(uri, method, (WebProxy) null, (NetworkCredential) null);
      }
      else
      {
        if (!(uri.Scheme == Uri.UriSchemeFile))
          throw new HtmlWebException("Unsupported uri scheme: '" + uri.Scheme + "'.");
        document = new HtmlDocument()
        {
          OptionAutoCloseOnEnd = false
        };
        document.OptionAutoCloseOnEnd = true;
        if (this.OverrideEncoding != null)
          document.Load(url, this.OverrideEncoding);
        else
          document.DetectEncodingAndLoad(url, this._autoDetectEncoding);
      }
      if (this.PreHandleDocument != null)
        this.PreHandleDocument(document);
      return document;
    }

    public HtmlDocument Load(string url, string method, WebProxy proxy, NetworkCredential credentials)
    {
      Uri uri = new Uri(url);
      HtmlDocument document;
      if (uri.Scheme == Uri.UriSchemeHttps || uri.Scheme == Uri.UriSchemeHttp)
      {
        document = this.LoadUrl(uri, method, proxy, credentials);
      }
      else
      {
        if (!(uri.Scheme == Uri.UriSchemeFile))
          throw new HtmlWebException("Unsupported uri scheme: '" + uri.Scheme + "'.");
        document = new HtmlDocument()
        {
          OptionAutoCloseOnEnd = false
        };
        document.OptionAutoCloseOnEnd = true;
        document.DetectEncodingAndLoad(url, this._autoDetectEncoding);
      }
      if (this.PreHandleDocument != null)
        this.PreHandleDocument(document);
      return document;
    }

    public void LoadHtmlAsXml(string htmlUrl, XmlTextWriter writer)
    {
      this.Load(htmlUrl).Save((XmlWriter) writer);
    }

    private static void FilePreparePath(string target)
    {
      if (System.IO.File.Exists(target))
      {
        FileAttributes attributes = System.IO.File.GetAttributes(target);
        System.IO.File.SetAttributes(target, attributes & ~FileAttributes.ReadOnly);
      }
      else
      {
        string directoryName = Path.GetDirectoryName(target);
        if (Directory.Exists(directoryName))
          return;
        Directory.CreateDirectory(directoryName);
      }
    }

    private static DateTime RemoveMilliseconds(DateTime t)
    {
      return new DateTime(t.Year, t.Month, t.Day, t.Hour, t.Minute, t.Second, 0);
    }

    private static long SaveStream(Stream stream, string path, DateTime touchDate, int streamBufferSize)
    {
      HtmlWeb.FilePreparePath(path);
      FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
      BinaryReader binaryReader = (BinaryReader) null;
      BinaryWriter binaryWriter = (BinaryWriter) null;
      long num = 0;
      try
      {
        binaryReader = new BinaryReader(stream);
        binaryWriter = new BinaryWriter((Stream) fileStream);
        byte[] buffer;
        do
        {
          buffer = binaryReader.ReadBytes(streamBufferSize);
          num += (long) buffer.Length;
          if (buffer.Length > 0)
            binaryWriter.Write(buffer);
        }
        while (buffer.Length > 0);
      }
      finally
      {
        binaryReader?.Close();
        if (binaryWriter != null)
        {
          binaryWriter.Flush();
          binaryWriter.Close();
        }
        fileStream?.Close();
      }
      System.IO.File.SetLastWriteTime(path, touchDate);
      return num;
    }

    private HttpStatusCode Get(Uri uri, string method, string path, HtmlDocument doc, IWebProxy proxy, ICredentials creds)
    {
      string str = (string) null;
      bool flag1 = false;
      HttpWebRequest request = WebRequest.Create(uri) as HttpWebRequest;
      request.Method = method;
      request.UserAgent = this.UserAgent;
      if (proxy != null)
      {
        if (creds != null)
        {
          proxy.Credentials = creds;
          request.Credentials = creds;
        }
        else
        {
          proxy.Credentials = CredentialCache.DefaultCredentials;
          request.Credentials = CredentialCache.DefaultCredentials;
        }
        request.Proxy = proxy;
      }
      this._fromCache = false;
      this._requestDuration = 0;
      int tickCount = Environment.TickCount;
      if (this.UsingCache)
      {
        str = this.GetCachePath(request.RequestUri);
        if (System.IO.File.Exists(str))
        {
          request.IfModifiedSince = System.IO.File.GetLastAccessTime(str);
          flag1 = true;
        }
      }
      if (this._cacheOnly)
      {
        if (!System.IO.File.Exists(str))
          throw new HtmlWebException("File was not found at cache path: '" + str + "'");
        if (path != null)
        {
          IOLibrary.CopyAlways(str, path);
          if (str != null)
            System.IO.File.SetLastWriteTime(path, System.IO.File.GetLastWriteTime(str));
        }
        this._fromCache = true;
        return HttpStatusCode.NotModified;
      }
      if (this._useCookies)
        request.CookieContainer = new CookieContainer();
      if (this.PreRequest != null)
      {
        if (!this.PreRequest(request))
          return HttpStatusCode.ResetContent;
      }
      HttpWebResponse response;
      try
      {
        response = request.GetResponse() as HttpWebResponse;
      }
      catch (WebException ex)
      {
        this._requestDuration = Environment.TickCount - tickCount;
        response = (HttpWebResponse) ex.Response;
        if (response == null)
        {
          if (flag1)
          {
            if (path != null)
            {
              IOLibrary.CopyAlways(str, path);
              System.IO.File.SetLastWriteTime(path, System.IO.File.GetLastWriteTime(str));
            }
            return HttpStatusCode.NotModified;
          }
          throw;
        }
      }
      catch (Exception ex)
      {
        this._requestDuration = Environment.TickCount - tickCount;
        throw;
      }
      if (this.PostResponse != null)
        this.PostResponse(request, response);
      this._requestDuration = Environment.TickCount - tickCount;
      this._responseUri = response.ResponseUri;
      bool flag2 = this.IsHtmlContent(response.ContentType);
      Encoding encoding = !string.IsNullOrEmpty(response.ContentEncoding) ? Encoding.GetEncoding(response.ContentEncoding) : (Encoding) null;
      if (this.OverrideEncoding != null)
        encoding = this.OverrideEncoding;
      if (response.StatusCode == HttpStatusCode.NotModified)
      {
        if (!this.UsingCache)
          throw new HtmlWebException("Server has send a NotModifed code, without cache enabled.");
        this._fromCache = true;
        if (path != null)
        {
          IOLibrary.CopyAlways(str, path);
          System.IO.File.SetLastWriteTime(path, System.IO.File.GetLastWriteTime(str));
        }
        return response.StatusCode;
      }
      Stream responseStream = response.GetResponseStream();
      if (responseStream != null)
      {
        if (this.UsingCache)
        {
          HtmlWeb.SaveStream(responseStream, str, HtmlWeb.RemoveMilliseconds(response.LastModified), this._streamBufferSize);
          this.SaveCacheHeaders(request.RequestUri, response);
          if (path != null)
          {
            IOLibrary.CopyAlways(str, path);
            System.IO.File.SetLastWriteTime(path, System.IO.File.GetLastWriteTime(str));
          }
        }
        else if (doc != null && flag2)
        {
          if (encoding == null)
            doc.Load(responseStream, true);
          else
            doc.Load(responseStream, encoding);
        }
        response.Close();
      }
      return response.StatusCode;
    }

    private string GetCacheHeader(Uri requestUri, string name, string def)
    {
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.Load(this.GetCacheHeadersPath(requestUri));
      XmlNode xmlNode = xmlDocument.SelectSingleNode("//h[translate(@n, 'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')='" + name.ToUpper() + "']");
      if (xmlNode == null)
        return def;
      return xmlNode.Attributes[name].Value;
    }

    private string GetCacheHeadersPath(Uri uri)
    {
      return this.GetCachePath(uri) + ".h.xml";
    }

    private bool IsCacheHtmlContent(string path)
    {
      return this.IsHtmlContent(HtmlWeb.GetContentTypeForExtension(Path.GetExtension(path), (string) null));
    }

    private bool IsHtmlContent(string contentType)
    {
      return contentType.ToLower().StartsWith("text/html");
    }

    private bool IsGZipEncoding(string contentEncoding)
    {
      return contentEncoding.ToLower().StartsWith("gzip");
    }

    private HtmlDocument LoadUrl(Uri uri, string method, WebProxy proxy, NetworkCredential creds)
    {
      HtmlDocument doc = new HtmlDocument();
      doc.OptionAutoCloseOnEnd = false;
      doc.OptionFixNestedTags = true;
      this._statusCode = this.Get(uri, method, (string) null, doc, (IWebProxy) proxy, (ICredentials) creds);
      if (this._statusCode == HttpStatusCode.NotModified)
        doc.DetectEncodingAndLoad(this.GetCachePath(uri));
      return doc;
    }

    private void SaveCacheHeaders(Uri requestUri, HttpWebResponse resp)
    {
      string cacheHeadersPath = this.GetCacheHeadersPath(requestUri);
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.LoadXml("<c></c>");
      XmlNode firstChild = xmlDocument.FirstChild;
      foreach (string header in (NameObjectCollectionBase) resp.Headers)
      {
        XmlNode element = (XmlNode) xmlDocument.CreateElement("h");
        XmlAttribute attribute1 = xmlDocument.CreateAttribute("n");
        attribute1.Value = header;
        element.Attributes.Append(attribute1);
        XmlAttribute attribute2 = xmlDocument.CreateAttribute("v");
        attribute2.Value = resp.Headers[header];
        element.Attributes.Append(attribute2);
        firstChild.AppendChild(element);
      }
      xmlDocument.Save(cacheHeadersPath);
    }

    public object CreateInstance(string htmlUrl, string xsltUrl, XsltArgumentList xsltArgs, Type type)
    {
      return this.CreateInstance(htmlUrl, xsltUrl, xsltArgs, type, (string) null);
    }

    public object CreateInstance(string htmlUrl, string xsltUrl, XsltArgumentList xsltArgs, Type type, string xmlPath)
    {
      StringWriter stringWriter = new StringWriter();
      XmlTextWriter writer = new XmlTextWriter((TextWriter) stringWriter);
      if (xsltUrl == null)
        this.LoadHtmlAsXml(htmlUrl, writer);
      else if (xmlPath == null)
        this.LoadHtmlAsXml(htmlUrl, xsltUrl, xsltArgs, writer);
      else
        this.LoadHtmlAsXml(htmlUrl, xsltUrl, xsltArgs, writer, xmlPath);
      writer.Flush();
      XmlTextReader xmlTextReader = new XmlTextReader((TextReader) new StringReader(stringWriter.ToString()));
      XmlSerializer xmlSerializer = new XmlSerializer(type);
      try
      {
        return xmlSerializer.Deserialize((XmlReader) xmlTextReader);
      }
      catch (InvalidOperationException ex)
      {
        throw new Exception(ex.ToString() + ", --- xml:" + (object) stringWriter);
      }
    }

    public void LoadHtmlAsXml(string htmlUrl, string xsltUrl, XsltArgumentList xsltArgs, XmlTextWriter writer)
    {
      this.LoadHtmlAsXml(htmlUrl, xsltUrl, xsltArgs, writer, (string) null);
    }

    public void LoadHtmlAsXml(string htmlUrl, string xsltUrl, XsltArgumentList xsltArgs, XmlTextWriter writer, string xmlPath)
    {
      if (htmlUrl == null)
        throw new ArgumentNullException(nameof (htmlUrl));
      HtmlDocument htmlDocument = this.Load(htmlUrl);
      if (xmlPath != null)
      {
        XmlTextWriter xmlTextWriter = new XmlTextWriter(xmlPath, htmlDocument.Encoding);
        htmlDocument.Save((XmlWriter) xmlTextWriter);
        xmlTextWriter.Close();
      }
      if (xsltArgs == null)
        xsltArgs = new XsltArgumentList();
      xsltArgs.AddParam("url", "", (object) htmlUrl);
      xsltArgs.AddParam("requestDuration", "", (object) this.RequestDuration);
      xsltArgs.AddParam("fromCache", "", (object) this.FromCache);
      XslCompiledTransform compiledTransform = new XslCompiledTransform();
      compiledTransform.Load(xsltUrl);
      compiledTransform.Transform((IXPathNavigable) htmlDocument, xsltArgs, (XmlWriter) writer);
    }

    public delegate void PostResponseHandler(HttpWebRequest request, HttpWebResponse response);

    public delegate void PreHandleDocumentHandler(HtmlDocument document);

    public delegate bool PreRequestHandler(HttpWebRequest request);
  }
}
