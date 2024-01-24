using SharpDX.D3DCompiler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCode3D.Models
{
    public class GCPicker
    {
        private static readonly ExplorerElement DefaultProgram = new() {
            Path = @"C:\PrimaPower\rd\GCode3D\GCode3D\resources\ATR_77_001_Fiera_NP.gcode"
        };
        private static readonly ExplorerElement DefaultFolder = new() {
            Path = @"C:\PrimaPower\rd\GCode3D\GCode3D\resources\",
            Type = ExplorerElementType.Folder
        };
        
        private ExplorerElement _CurrentFolder = DefaultFolder;
        public ExplorerElement CurrentFolder
        {
            get => _CurrentFolder;
            set
            {
                _CurrentFolder = value;
                if(!string.IsNullOrEmpty(value.Path))
                    Watcher.Path = value.Path;
            }
        }
        
        public ExplorerElement CurrentFile { get; set; } = DefaultProgram;
        
        public FileSystemWatcher Watcher { get; private set; } = new()
        {
            Path = DefaultFolder.Path,
            Filter = "*.gcode",
            EnableRaisingEvents = true,
            NotifyFilter = NotifyFilters.Attributes
                            | NotifyFilters.CreationTime
                            | NotifyFilters.DirectoryName
                            | NotifyFilters.FileName
                            | NotifyFilters.LastAccess
                            | NotifyFilters.LastWrite
                            | NotifyFilters.Security
                            | NotifyFilters.Size,
        };
        
        public List<ExplorerElement> Folders
        {
            get => [.. Directory.GetDirectories(CurrentFolder.Path, "*", SearchOption.AllDirectories)
                .Select(filename => new ExplorerElement
                {
                    Path = filename,
                    Type = ExplorerElementType.Folder
                })];
        }

        public List<ExplorerElement> Files
        {
            get => [.. Directory.GetFiles(CurrentFolder.Path)
                .Select(path => path.Split("\\").LastOrDefault() ?? string.Empty)
                .Select(filename => new ExplorerElement
                {
                    Path = filename,
                    Type = ExplorerElementType.File
                })];
        }

        public List<ExplorerElement> Content {
            get => [.. Folders, .. Files];
        }
    }
}
