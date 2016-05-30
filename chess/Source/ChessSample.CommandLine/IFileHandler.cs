namespace ChessSample.CommandLine
{
    interface IFileHandler
    {
        InputData ParseInputFile(string path);

        void WriteOutputFile(string path, OutputData data);
    }
}
