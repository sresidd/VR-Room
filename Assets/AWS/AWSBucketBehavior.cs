using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class AWSBucketBehavior : MonoBehaviour
{

    //Bucket Information
    #region  key_info
    private const string awsBucketName = "unity-data-assets";
    private const string awsAccessKey = "AKIAT3JYOE6EZ7VUURGZ";
    private const string awsSecretKey = "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";
    #endregion

    private string dataFileName = "";
    private string dataDirPath = "";
    public AWSBucketBehavior(string dataDirPath, string dataFileName){
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public void UploadFileToAWS3(){

        //AWS Header Creation
        string currentAWS3Date = System.DateTime.UtcNow.ToString("ddd, dd MMM yyyy HH:mm:ss ") + "GMT";
        string canonicalString = "PUT\n\n\n\nx-amz-date:" + currentAWS3Date + "\n/" + awsBucketName + "/" + dataFileName;

        //Canonical Encoding
        UTF8Encoding encode = new UTF8Encoding();
        HMACSHA1 signature = new HMACSHA1();
        signature.Key = encode.GetBytes(awsSecretKey);
        byte[] bytes = encode.GetBytes(canonicalString);
        byte[] moreBytes = signature.ComputeHash(bytes);
        string encodedCanonical = Convert.ToBase64String(moreBytes);

        //AWS Header
        string aws3Header = "AWS " + awsAccessKey + ":" + encodedCanonical;

        //Destination path
        string awsURLBaseVirtual = "https://" + awsBucketName + ".s3.amazonaws.com/"; 
        string URL3 = awsURLBaseVirtual + dataFileName;

        //Web request with AWS Header 
        WebRequest requestS3 = (HttpWebRequest)WebRequest.Create(URL3); requestS3.Headers.Add("Authorization", aws3Header);
        requestS3.Headers.Add("x-amz-date", currentAWS3Date);

        //Read Byte data from local file
        byte[] fileRawBytes = File.ReadAllBytes(dataDirPath + "/" + dataFileName);
        requestS3.ContentLength = fileRawBytes.Length;

        //Using PUT as RESTful method
        requestS3.Method = "PUT";

        //Upload the file to the bucket via a stream
        Stream S3Stream = requestS3.GetRequestStream();
        S3Stream.Write(fileRawBytes, 0, fileRawBytes.Length);
        Debug.Log(
        "Sent bytes: " +
        requestS3.ContentLength +
        ", for file: " +
        dataFileName);

        S3Stream.Close();
    }
}
