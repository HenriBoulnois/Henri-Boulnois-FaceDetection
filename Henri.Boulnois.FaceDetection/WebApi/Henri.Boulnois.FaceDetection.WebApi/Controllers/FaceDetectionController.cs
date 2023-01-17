using Microsoft.AspNetCore.Mvc;

namespace Henri.Boulnois.FaceDetection.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class FaceDetectionController : ControllerBase
{
    private readonly FaceDetection _faceDetection;

    public FaceDetectionController(FaceDetection faceDetection)
    {
        _faceDetection = faceDetection;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> OnPostUploadAsync(List<IFormFile> files)
    {
        if (files.Count != 1)
            return BadRequest();
        using var sceneSourceStream = files[0].OpenReadStream();
        using var sceneMemoryStream = new MemoryStream();
        sceneSourceStream.CopyTo(sceneMemoryStream);
        var imageSceneData = sceneMemoryStream.ToArray();
        var detectObjectInScenesResults = new
            FaceDetection().FaceDetectionInScene(imageSceneData);
        return File(detectObjectInScenesResults.ImageData, "image/png");
    }
}
