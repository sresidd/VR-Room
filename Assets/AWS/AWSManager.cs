using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Amazon.S3;
using Amazon;
using Amazon.S3.Model;
using Amazon.CognitoIdentity;


public class AWSManager : Singleton<AWSManager>
{
    public string S3Region = RegionEndpoint.EUWest1.SystemName;

    private RegionEndpoint _S3Region{
        get { return RegionEndpoint.GetBySystemName(S3Region);}
    }

    private AmazonS3Client _s3Client;
    // private AmazonS3Client S3Client{
        // get {
        //     if(_s3Client == null){
        //         _s3Client = new AmazonS3Client(new CognitoAWSCredentials(

        //         ))
        //     }
        // }
    // }
}
