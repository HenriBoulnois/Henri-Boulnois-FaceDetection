using System.Reflection;
using System.Text.Json;
using Xunit;

namespace Henri.Boulnois.FaceDetection.tests;

public class FaceDetectionUnitTest
{
    [Fact]
    public async Task ObjectShouldBeDetectedCorrectly()
    {
        var executingPath = GetExecutingPath();
        
        var imageScenesData = new List<byte[]>();
        foreach (var imagePath in
                 Directory.EnumerateFiles(Path.Combine(executingPath, "Scenes")))
        {
            var imageBytes = await File.ReadAllBytesAsync(imagePath);
            imageScenesData.Add(imageBytes);
        }
        var detectObjectInScenesResults = new
            FaceDetection().DetectInScenes(imageScenesData);
        Assert.Equal("[{\"X\":408,\"Y\":70},{\"X\":321,\"Y\":100},{\"X\":491,\"Y\":102},{\"X\":195,\"Y\":106}]",JsonSerializer.Serialize(detectObjectInScenesResults[1].Points));
        Assert.Equal("[{\"X\":554,\"Y\":175},{\"X\":472,\"Y\":84}]",JsonSerializer.Serialize(detectObjectInScenesResults[0].Points));
        Assert.Equal("[{\"X\":228,\"Y\":129},{\"X\":112,\"Y\":82}]",JsonSerializer.Serialize(detectObjectInScenesResults[2].Points));
    }
    private static string GetExecutingPath()
    {
        var executingAssemblyPath = Assembly.GetExecutingAssembly().Location;
        var executingPath = Path.GetDirectoryName(executingAssemblyPath);
        return executingPath;
    }
}