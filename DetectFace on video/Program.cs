using System;
using System.Threading;
using Amazon.Rekognition.Model;
namespace DetectFace_on_video
{
    public class Program
    {
        static void Main(string[] args)
        {
            DetectFace();
           
        }
        public static void DetectFace()
        {
            var awsCredentials = new Amazon.Runtime.BasicAWSCredentials("AKIA2SEVNCMGZ3GCJV5X", "zYAzTTLzYYGrr/tZ1E4Vw4MRSF1UA+uw47Fqn6yi");

            var rekognition = new Amazon.Rekognition.AmazonRekognitionClient(awsCreden‌tials, Amazon.RegionEndpoint.USEast1);
            var numb = 0;
            var res = rekognition.StartFaceDetectionAsync(new Amazon.Rekognition.Model.StartFaceDetectionRequest() { Video = new Amazon.Rekognition.Model.Video() { S3Object = new Amazon.Rekognition.Model.S3Object() { Bucket = "idfcbones", Name = "videoplayback.mp4" } } }).Result;
            GetFaceDetectionResponse result;
            do
            {
                Thread.Sleep(4000);
                result = rekognition.GetFaceDetectionAsync(new Amazon.Rekognition.Model.GetFaceDetectionRequest() { JobId = res.JobId }).Result;
                if (result.JobStatus == Amazon.Rekognition.VideoJobStatus.SUCCEEDED)
                {
                    break;
                }
            } while (true);

            result.Faces.ForEach(x => Console.WriteLine($"[{numb++}]Boundbox'X'-{x.Face.BoundingBox.Width}\nBoundbox'Y'-{x.Face.BoundingBox.Height}\n"));
            
        }
    }
}
