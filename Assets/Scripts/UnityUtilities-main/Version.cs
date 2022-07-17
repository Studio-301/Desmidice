//
//   2022 - Studio 301 s.r.o
//   Maintainer: Kirill Tiuliusin
//   Description: Stores product versions in format 'XX.XX.X' where X is digit, e.g 1.02.0
//      yet when declaring version you can still use formats like 1, 1.1
//      keep in mind that 1.01.0 == 1.01 == 1.1 != 1.10
//   
//   Definition: Version ver = "1.01"
//            or Version ver = new Version("1.01")
//
//   Comparison: ver == "1.1"
//            or ver == new Version ("1.1")
//   when comparing versions you can use >, >=, <, <=, != as well
//      
//////////////////////////////////////////////

using System;

public class Version
{
    public readonly int major = 0; // from 0 to 99
    public readonly int minor = 0; // from 0 to 99
    public readonly int patch = 0; // from 0 to 9


    public Version(string version = "0")
    {
        string[] components = version.Split('.');
        if (components.Length > 3)
            throw new Exception("Version should have max 3 components");
        if (!int.TryParse(components[0], out major) || major < 0 || major > 99)
            throw new Exception("Version's major should be an integer from 0 to 99");
        if (components.Length == 1)
            return;
        if (!int.TryParse(components[1], out minor) || minor < 0 || minor > 99)
            throw new Exception("Version's minor should be an integer from 0 to 99");
        if (components.Length == 2)
            return;
        if (!int.TryParse(components[2], out patch) || patch < 0 || patch > 9)
            throw new Exception("Version's patch should be an integer from 0 to 9");
    }

    public override string ToString()
    {
        return $"{major}.{minor:00}.{patch:0}";
    }

    public int GetBundleVersionCode()
    {
        return major * 1000 + minor * 10 + patch;
    }

    // return -1 if A is lesser than B, 1 if the A is greater then B, and 0 if they are equal
    public static int Compare(Version A, Version B)
    {
        if (A.major > B.major)
            return 1;
        if (A.minor < B.minor)
            return -1;
        if (A.minor > B.minor)
            return 1;
        if (A.minor < B.minor)
            return -1;
        if (A.patch > B.patch)
            return 1;
        if (A.patch < B.patch)
            return -1;
        return 0;
    }

    public static implicit operator Version(string version) => new Version(version);

    public bool IsGreaterThan(Version another) => Compare(this, another) > 0;
    public bool IsEqualTo(Version another) => Compare(this, another) == 0;
    public bool IsLesserThan(Version another) => Compare(this, another) < 0;

    public static bool operator >(Version A, Version B) => A.IsGreaterThan(B);
    public static bool operator <(Version A, Version B) => A.IsLesserThan(B);

    public static bool operator >=(Version A, Version B) => A.IsGreaterThan(B) || A.IsEqualTo(B);
    public static bool operator <=(Version A, Version B) => A.IsLesserThan(B) || A.IsEqualTo(B);

    public static bool operator ==(Version A, Version B) => A.IsEqualTo(B);
    public static bool operator !=(Version A, Version B) => !A.IsEqualTo(B);
}
