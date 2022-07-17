//
//   2022 - Studio 301 s.r.o
//   Maintainer: Kirill Tiuliusin
//   Description: Transforms and stores graphics in our own format
//      
//////////////////////////////////////////////

using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public class IMG301
{
    public const string FORMAT_HEADER = "IMG301"; // indicator of our graphics format
    public const byte FORMAT_VERSION = 1;

    // Format description:
    // bytes 0-5: IMG301
    // byte 6: format version = 1
    // bytes 7-10: TextureFormat as int
    // bytes 11-12: texture width as ushort
    // bytes 12-13: texture height as ushort
    // bytes 14-17: mipmapCount as int
    // from byte 18: raw texture data

    private Texture2D image;

    public IMG301()
    {
        image = new Texture2D(16, 16, TextureFormat.RGBA32, false);
    }

    public IMG301(Texture2D texture)
    {
        image = new Texture2D(texture.width, texture.height, texture.format, texture.mipmapCount > 1);
        Graphics.CopyTexture(texture, image);
    }

    // pass byte array of PNG/JPG or IMG301 file
    // useMipMaps is only applied when passing PNG/JPG, settings from IMG301 file is used otherwise
    public IMG301(byte[] imageData, bool useMipMaps = false)
    {
        if (IsIn301Format(imageData))
        {
            image = GetTexture(imageData);
            return;
        }

        // not in 301 format, just png/jpeg
        image = new Texture2D(2, 2);
        if (!image.LoadImage(imageData, useMipMaps))
        {
            throw new Exception("[IMG301] Error loading image");
            return;
        }

        image.Apply();
    }

#if UNITY_EDITOR
    public Texture2D ChangeFormat(TextureFormat newFormat)
    {
        image = ChangeFormat(image, newFormat);
        return image;
    }
#endif

    public byte[] GetImageData()
    {
        return GetImageData(image);
    }

    public Texture2D GetTexture()
    {
        return image;
    }

    public byte[] GetPNG()
    {
        return image.EncodeToPNG();
    }

    public byte[] GetJPG()
    {
        return image.EncodeToJPG();
    }

    #region Static Methods

#if UNITY_EDITOR
    public static Texture2D ChangeFormat(Texture2D original, TextureFormat newFormat)
    {
        Texture2D transformedTexture = new Texture2D(original.width, original.height, TextureFormat.RGBA32, original.mipmapCount > 1);
        transformedTexture.SetPixels(original.GetPixels());
        EditorUtility.CompressTexture(transformedTexture, newFormat, TextureCompressionQuality.Normal);
        transformedTexture.Apply();
        return transformedTexture;
    }
#endif

    public static byte[] GetImageData(Texture2D texture)
    {
        byte[] header = Encoding.ASCII.GetBytes(FORMAT_HEADER);
        byte[] format = BitConverter.GetBytes((int)texture.format);
        byte[] width = BitConverter.GetBytes((ushort)texture.width);
        byte[] height = BitConverter.GetBytes((ushort)texture.height);
        byte[] mipmapCount = BitConverter.GetBytes(texture.mipmapCount);

        MemoryStream stream = new MemoryStream();
        stream.Write(header);
        stream.WriteByte(FORMAT_VERSION);
        stream.Write(format);
        stream.Write(width);
        stream.Write(height);
        stream.Write(mipmapCount);
        stream.Write(texture.GetRawTextureData());
        stream.Close();

        return stream.ToArray();
    }

    public static Texture2D GetTexture(byte[] imageData)
    {
        MemoryStream stream = new MemoryStream(imageData);
        int offset = 0;
        byte[] header = new byte[6];
        offset += stream.Read(header, 0, 6);
        if (Encoding.ASCII.GetString(header) != FORMAT_HEADER)
            throw new Exception("[IMG301] You're trying to get texture form the file which is not in 301 texture format");
        byte version = (byte)stream.ReadByte();
        offset++;
        if (version != FORMAT_VERSION)
            throw new Exception(String.Format(
                "[IMG301] Image file was created with different format version (FileVersion={},HandlerVersion={})",
                version, FORMAT_VERSION));
        byte[] formatBuffer = new byte[sizeof(int)];
        offset += stream.Read(formatBuffer, 0, sizeof(int));
        TextureFormat format = (TextureFormat)BitConverter.ToInt32(formatBuffer);
        byte[] sizeBuffer = new byte[sizeof(ushort)];
        offset += stream.Read(sizeBuffer, 0, sizeBuffer.Length);
        ushort width = BitConverter.ToUInt16(sizeBuffer);
        offset += stream.Read(sizeBuffer, 0, sizeBuffer.Length);
        ushort height = BitConverter.ToUInt16(sizeBuffer);
        byte[] mipmapBuffer = new byte[sizeof(int)];
        offset += stream.Read(mipmapBuffer, 0, mipmapBuffer.Length);
        int mipmapCount = BitConverter.ToInt32(mipmapBuffer);
        int dataSize = imageData.Length - offset;
        byte[] data = new byte[dataSize];
        offset += stream.Read(data, 0, imageData.Length - offset);
        stream.Close();
        Texture2D texture = new Texture2D(width, height, format, mipmapCount > 1);
        texture.LoadRawTextureData(data);
        texture.Apply();
        return texture;
    }

    public static bool IsIn301Format(byte[] data)
    {
        MemoryStream stream = new MemoryStream(data);
        byte[] header = new byte[6];
        stream.Read(header, 0, 6);
        stream.Close();

        return Encoding.ASCII.GetString(header) == FORMAT_HEADER;
    }

    #endregion
}

public enum GraphicsType
{
    None, Texture, Image, IMG301, Unknown
}
