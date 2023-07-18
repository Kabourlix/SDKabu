namespace SDKabu.KUtils;

public static class KFileUtils
{
    #region Creation

    /// <summary>
    /// Try create a new file at given location (no overwrite).
    /// </summary>
    /// <param name="_name">The name of the file with extension.</param>
    /// <param name="_location">The path to the directory it should be stored.</param>
    /// <param name="_reformat">Whether or not the given name should be trimmed and replaced its whitespaces</param>
    /// <returns>true if the creation is successful, false otherwise.</returns>
    public static bool TryCreateFile(string _name, string _location, bool _reformat = true)
    {
        if (_reformat)
        {
            _name.ReformatFileName();
        } 
        
        var path = Path.Combine(_location, _name);
        if (File.Exists(path))
        {
            Console.WriteLine($"File {_name} already exists in {_location}");
            return false;
        }
        
        File.Create(path);
        return true;
    }

    /// <summary>
    /// Try create a new file at given location (no overwrite).
    /// </summary>
    /// <param name="_name">The name of the directory.</param>
    /// <param name="_location">The path to the parent directory it should be stored.</param>
    /// <param name="_reformat">Whether or not the given name should be trimmed and replaced its whitespaces</param>
    /// <returns></returns>
    public static bool TryCreateDirectory(string _name, string _location, bool _reformat = true)
    {
        if (_reformat)
        {
            _name.ReformatDirectoryName();
        }
        
        var path = Path.Combine(_location, _name);
        if (Directory.Exists(path))
        {
            Console.WriteLine($"Directory {_name} already exists in {_location}");
            return false;
        }
        
        Directory.CreateDirectory(path);
        return true;
    }

    #endregion //Creation

    #region Name checking

    private static bool IsFileNameCorrect(string _name)
    {
        var split = _name.Split('.');
        
        if (split.Length == 2) return true;
        
        Console.WriteLine($"File {_name} is not a valid file name.");
        return false;

    }

    private static void ReformatFileName(this string _name)
    {
        if (!IsFileNameCorrect(_name))
        {
            throw new Exception($"File {_name} is not a valid file name.");
        }
        _name = _name.Trim().Replace(" ", "_");
    }

    private static void ReformatDirectoryName(this string _name)
    {
        _name = _name.Trim().Replace(" ", "_");
    }

    #endregion //Name checking
}