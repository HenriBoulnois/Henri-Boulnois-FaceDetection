// See https://aka.ms/new-console-template for more information

using System.Runtime.CompilerServices;

namespace Henri.Boulnois.FaceDetection.console;

using System.Reflection;
using System.Text.Json;
using Henri.Boulnois.FaceDetection;


class Program
{
    static void Main(string[] args)
    {
        DetectFace(args);
    }
    private static void DetectFace(string[] images)
    {
        var imageScenesData = new List<byte[]>();
        foreach (var imagePath in
                 images)
        {
            var imageBytes = File.ReadAllBytes(imagePath);
            imageScenesData.Add(imageBytes);

        }
        var detectFaceInScenesResults = new
            FaceDetection().DetectInScenes(imageScenesData);
        foreach (var detectionResult in detectFaceInScenesResults)
        {
            System.Console.WriteLine($"Points:{JsonSerializer.Serialize(detectionResult.Points)}");
        }
    }
}