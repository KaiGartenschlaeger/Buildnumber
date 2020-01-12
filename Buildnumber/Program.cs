using System;
using System.IO;

namespace Buildnumber
{
    class Program
    {
        //
        // Arguments:
        //
        // 1: Target filename
        // 2: Format (optional)
        //
        static int Main(string[] args)
        {
            ResultCode result;

            if (args.Length >= 1)
            {
                string filename = args[0];

                string format = null;
                if (args.Length >= 2)
                {
                    format = args[1];
                }

                if (string.IsNullOrEmpty(format))
                {
                    format = "yyyy-MM-dd HH:ss:mm";
                }

                if (!Directory.Exists(Path.GetDirectoryName(filename)))
                {
                    Console.WriteLine("Directory '{0}' does not exist.", Path.GetDirectoryName(filename));
                    result = ResultCode.WrongOutputDirectory;
                }
                else
                {
                    try
                    {
                        using (StreamWriter writer = new StreamWriter(File.Create(filename), System.Text.Encoding.UTF8))
                        {
                            writer.WriteLine(DateTime.Now.ToString(format));
                        }

                        Console.WriteLine("Successfull created file '{0}'.", filename);
                        result = ResultCode.OK;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Wrong date format: {0}", format);
                        result = ResultCode.WrongFormat;

                        try
                        {
                            if (File.Exists(filename))
                            {
                                File.Delete(filename);
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Failed to delete file '{0}'", filename);
                            result = ResultCode.FailedToDeleteFile;
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Failed to create file '{0}'", filename);
                        result = ResultCode.FailedToCreateFile;
                    }
                }
            }
            else
            {
                Console.WriteLine("Syntax error:\nBuildnumber.exe <output filename> <date format>");
                result = ResultCode.SyntaxError;
            }

            return (int)result;
        }
    }
}