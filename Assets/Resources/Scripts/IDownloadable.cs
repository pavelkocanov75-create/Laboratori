using System;

public interface IDownloadable
{ 
    event Action DownloadFailed;
}
