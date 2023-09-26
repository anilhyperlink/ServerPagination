using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerPagination.Models;
using ServerPagination.Models.Comman;
using ServerPagination.Services.Abstraction;
using System;
using System.IO.Compression;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ServerPagination.API.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class HomeAPIController : Controller
    {
        private readonly IHomeHelper _homeHelper;
        public HomeAPIController(IHomeHelper homeHelper)
        {
            _homeHelper = homeHelper;
        }

        // Listing user data
        [Route("userlist")]
        [HttpGet]
        public IActionResult UserList(SetPagination setPagination)
        {
            Response response = new();
            try
            {
                var userdata = _homeHelper.UserList(setPagination);
                if (userdata != null)
                {
                    response.code = StatusCodes.Status200OK;
                    response.status = true;
                    response.message = "Success";
                    response.data = userdata;
                    return Ok(response);
                }
                response.code = StatusCodes.Status400BadRequest;
                response.status = false;
                response.message = "Object is null.";
                return BadRequest(response);
            }
            catch (Exception e)
            {
                response.code = StatusCodes.Status500InternalServerError;
                response.status = false;
                response.message = "Something went wrong." + e.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        // add user data
        [Route("adduser")]
        [HttpGet]
        public IActionResult AddUser(UserModel user)
        {
            Response response = new();
            try
            {
                var userdata = "";
                _homeHelper.AddUser(user);
                if (userdata != null)
                {
                    response.code = StatusCodes.Status200OK;
                    response.status = true;
                    response.message = "Success";
                    response.data = userdata;
                    return Ok(response);
                }
                response.code = StatusCodes.Status400BadRequest;
                response.status = false;
                response.message = "Object is null.";
                return BadRequest(response);
            }
            catch (Exception e)
            {
                response.code = StatusCodes.Status500InternalServerError;
                response.status = false;
                response.message = "Something went wrong." + e.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        // delete user data
        [Route("deleteuser{UserId}")]
        [HttpDelete]
        public IActionResult deleteUser(int UserId)
        {
            Response response = new();
            try
            {
                var userdata = "";
                _homeHelper.DeleteUser(UserId);
                if (userdata != null)
                {
                    response.code = StatusCodes.Status200OK;
                    response.status = true;
                    response.message = "Success";
                    response.data = userdata;
                    return Ok(response);
                }
                response.code = StatusCodes.Status400BadRequest;
                response.status = false;
                response.message = "Object is null.";
                return BadRequest(response);
            }
            catch (Exception e)
            {
                response.code = StatusCodes.Status500InternalServerError;
                response.status = false;
                response.message = "Something went wrong." + e.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }


        // delete user data
        [Route("activemanage{UserId}")]
        [HttpGet]
        public IActionResult ActiveManage(int UserId)
        {
            Response response = new();
            try
            {
                var userdata = "";
                _homeHelper.ActiveManage(UserId);
                if (userdata != null)
                {
                    response.code = StatusCodes.Status200OK;
                    response.status = true;
                    response.message = "Success";
                    response.data = userdata;
                    return Ok(response);
                }
                response.code = StatusCodes.Status400BadRequest;
                response.status = false;
                response.message = "Object is null.";
                return BadRequest(response);
            }
            catch (Exception e)
            {
                response.code = StatusCodes.Status500InternalServerError;
                response.status = false;
                response.message = "Something went wrong." + e.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        // delete user data
        [Route("getUser{UserId}")]
        [HttpGet]
        public IActionResult GetUser(int UserId)
        {
            Response response = new();
            try
            {
                var userdata = _homeHelper.GetUser(UserId);
                if (userdata != null)
                {
                    response.code = StatusCodes.Status200OK;
                    response.status = true;
                    response.message = "Success";
                    response.data = userdata;
                    return Ok(response);
                }
                response.code = StatusCodes.Status400BadRequest;
                response.status = false;
                response.message = "Object is null.";
                return BadRequest(response);
            }
            catch (Exception e)
            {
                response.code = StatusCodes.Status500InternalServerError;
                response.status = false;
                response.message = "Something went wrong." + e.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }



        // add user data
        [Route("editUser")]
        [HttpPut]
        public IActionResult EditUser(UserModel user)
        {
            Response response = new();
            try
            {
                var userdata = "";
                _homeHelper.EditUser(user);
                if (userdata != null)
                {
                    response.code = StatusCodes.Status200OK;
                    response.status = true;
                    response.message = "Success";
                    response.data = userdata;
                    return Ok(response);
                }
                response.code = StatusCodes.Status400BadRequest;
                response.status = false;
                response.message = "Object is null.";
                return BadRequest(response);
            }
            catch (Exception e)
            {
                response.code = StatusCodes.Status500InternalServerError;
                response.status = false;
                response.message = "Something went wrong." + e.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [Route("Encrpt")]
        [HttpGet]
        public IActionResult Encrpt(string FromDate = "", string ToDate = "")
        {
            Response response = new();
            try
            {
                string mysecurityKey = "48FA52CF47FF8083E8760BC9EF9106CEB83D4A592528F9B6";
                //string Response;
                string str = string.Empty;
                string strTop = string.Empty;
                string strBottom = string.Empty;
                string strCredential = string.Empty;
                string currentDate = string.Empty;
                string toDate = string.Empty;
                if (!string.IsNullOrEmpty(FromDate) && !string.IsNullOrEmpty(ToDate))
                {
                    currentDate = FromDate;
                    toDate = Convert.ToDateTime(ToDate).Date.AddDays(1).ToString("yyyy-MM-dd");
                }
                else
                {
                    currentDate = DateTime.Now.Date.ToString("yyyy-MM-dd");
                    toDate = DateTime.Now.Date.AddDays(1).ToString("yyyy-MM-dd");
                }
                #region "Generate Request String"
                str = @"<Grid type=""subscriber""><Conditions>
                        <Condition><Single><DataElement>is_unsubscriber</DataElement><DataValue>1</DataValue>
                        <Comparator>EQ</Comparator></Single></Condition>
                        <Condition><Single><DataElement>cancellation_date</DataElement><DataValue>" + currentDate + "</DataValue>" +
                        "<Comparator>GTE</Comparator></Single></Condition>" +
                        "<Condition><Single><DataElement>cancellation_date</DataElement><DataValue>" + toDate + "</DataValue>" +
                        "<Comparator>LTE</Comparator></Single></Condition></Conditions>" +
                        "<ReturnFields><DataElement>email</DataElement></ReturnFields></Grid>";

                strTop = @"<s:Envelope xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/""><s:Body><PostInformzMessage xmlns=""http://partner.informz.net/aapi/2009/08/""><x>";

                strCredential = @"<GridRequest xmlns=""http://partner.informz.net/aapi/2009/08/""><Password>Inf0rmzP@ssword</Password><Brand id=""1257"">Brand Name</Brand><User>ritesh</User><Grids>";
                string strBottomDecrypt = @"</Grids></GridRequest>";

                strBottom = @"</x></PostInformzMessage></s:Body></s:Envelope>";

                string requestLog = "Request string to get unsubscribe email addresses.<br/><br/>" +
                                    strTop + strCredential + str + strBottomDecrypt + strBottom;

                str = CompressData(str);// ouput string 
                str = EncryptData(mysecurityKey, "FAB87F0C88234FC5", str);
                //str = strTop + HttpUtility.HtmlEncode(strCredential) + str + HttpUtility.HtmlEncode(strBottomDecrypt) + strBottom;
                #endregion
                str = DecryptData(mysecurityKey, "FAB87F0C88234FC5", str);
                str = DecompressData(str);

                response.code = StatusCodes.Status200OK;
                response.status = true;
                response.message = "Success";
                response.data = str;
                return Ok(response);

            }
            catch (Exception e)
            {
                response.code = StatusCodes.Status500InternalServerError;
                response.status = false;
                response.message = "Something went wrong." + e.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
        public static string CompressData(string data)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                byte[] plainBytes = Encoding.UTF8.GetBytes(data);

                using (GZipStream zipStream = new GZipStream(memoryStream, CompressionMode.Compress, leaveOpen: true))
                {
                    zipStream.Write(plainBytes, 0, plainBytes.Length);
                }

                memoryStream.Position = 0;

                byte[] compressedBytes = new byte[memoryStream.Length + 4];

                Buffer.BlockCopy(
                    BitConverter.GetBytes(plainBytes.Length),
                    0,
                    compressedBytes,
                    0,
                    4
                );

                //Add the header, which is the length of the compressed message.
                memoryStream.Read(compressedBytes, 4, (int)memoryStream.Length);

                string compressedXml = Convert.ToBase64String(compressedBytes);

                return compressedXml;
            }
        }
        public static string EncryptData(string encryptionKey, string encryptionIv, string data)
        {
            using SymmetricAlgorithm symmetricAlgorithm = new TripleDESCryptoServiceProvider
            {
                Key = ConvertHexStringToByteArray(encryptionKey),
                IV = ConvertHexStringToByteArray(encryptionIv)
            };
            using MemoryStream memoryStream = new MemoryStream(data.Length * 2);
            using CryptoStream cryptoStream = new CryptoStream(memoryStream, symmetricAlgorithm.CreateEncryptor(), CryptoStreamMode.Write);
            byte[] plainBytes = Encoding.UTF8.GetBytes(data);

            cryptoStream.Write(plainBytes, 0, plainBytes.Length);
            cryptoStream.FlushFinalBlock();

            byte[] encryptedBytes = new byte[memoryStream.Length];
            memoryStream.Position = 0;

            memoryStream.Read(encryptedBytes, 0, (int)memoryStream.Length);

            string encryptedXml = Convert.ToBase64String(encryptedBytes);

            return encryptedXml;
        }
        private static byte[] ConvertHexStringToByteArray(string hexData)
        {
            //Pad the string with a 0 so it is divisible by 2.
            if (hexData.Length % 2 == 1)
            {
                hexData = "0" + hexData;
            }

            int byteArraySize = hexData.Length / 2;

            byte[] bytes = new byte[byteArraySize];

            for (int i = 0; i < byteArraySize; i++)
            {
                //Every two hex digits are one byte digit.
                string hexDigitToConvert = hexData.Substring(i * 2, 2);

                bytes[i] = Convert.ToByte(hexDigitToConvert, 16);
            }

            return bytes;
        }
        public static string DecompressData(string compressedData)
        {
            using MemoryStream memoryStream = new MemoryStream();
            byte[] compressedBytes = Convert.FromBase64String(compressedData);

            int messageLength = BitConverter.ToInt32(compressedBytes, 0);

            memoryStream.Write(compressedBytes, 4, compressedBytes.Length - 4);

            byte[] plainBytes = new byte[messageLength];

            memoryStream.Position = 0;

            using (GZipStream zipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
            {
                zipStream.Read(plainBytes, 0, plainBytes.Length);
            }

            string decompressedXml = Encoding.UTF8.GetString(plainBytes);

            return decompressedXml;
        }
        public static string DecryptData(string encryptionKey, string encryptionIv, string encryptedData)
        {
            using SymmetricAlgorithm symmetricAlgorithm = new TripleDESCryptoServiceProvider
            {
                Key = ConvertHexStringToByteArray(encryptionKey),
                IV = ConvertHexStringToByteArray(encryptionIv)
            };
            using MemoryStream memoryStream = new MemoryStream(encryptedData.Length);
            using CryptoStream cryptoStream = new CryptoStream(memoryStream, symmetricAlgorithm.CreateDecryptor(), CryptoStreamMode.Write);
            byte[] encryptedBytes = Convert.FromBase64String(encryptedData);

            cryptoStream.Write(encryptedBytes, 0, encryptedBytes.Length);
            cryptoStream.FlushFinalBlock();

            byte[] plainBytes = new byte[memoryStream.Length];

            memoryStream.Position = 0;
            memoryStream.Read(plainBytes, 0, (int)memoryStream.Length);

            string decryptedXml = Encoding.UTF8.GetString(plainBytes);

            return decryptedXml;
        }
    }
}