namespace Buildnumber
{
    enum ResultCode
    {
        OK = 0,

        SyntaxError,
        WrongOutputDirectory,
        FailedToDeleteFile,
        FailedToCreateFile,
        WrongFormat
    }
}