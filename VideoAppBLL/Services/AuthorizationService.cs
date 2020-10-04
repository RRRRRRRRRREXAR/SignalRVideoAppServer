﻿
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using VideoAppBLL.DTO;
using VideoAppBLL.Interfaces;
using VideoAppBLL.MapProfiles;
using VideoAppDAL.Entities;
using VideoAppDAL.Interfaces;

namespace VideoAppBLL.Services
{
    public class AuthorizationService:IAuthorizationService
    {
        private readonly IUnitOfWork unit;
        readonly MapperConfiguration mapConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new BllMapProfile());
        });
        public AuthorizationService(IUnitOfWork unit)
        {
            this.unit = unit;
        }

        public async Task<UserDTO> Login(string password, string email)
        {
            var mapper = new Mapper(mapConfig);
            UserDTO foundedUser = mapper.Map<UserDTO>(await unit.Repository<User>().Find(us => us.Email == email));
            if (VerifyHash(password, foundedUser.Password))
            {
                return foundedUser;
            }
            return null;
        }

        public async Task Register(UserDTO user)
        {
            var mapper = new Mapper(mapConfig);
            user.Password = ComputeHash(user.Password, null);
            user.ProfileImage = new ImageDTO { Link = @"https://localhost:44340\Images\default.jpeg", User = user };
            await unit.Repository<User>().Create(mapper.Map<User>(user));
            unit.Save();
        }

        public string ComputeHash(string plainText,
                                     byte[] saltBytes)
        {
            // If salt is not specified, generate it on the fly.
            if (saltBytes == null)
            {
                // Define min and max salt sizes.
                int minSaltSize = 4;
                int maxSaltSize = 8;

                // Generate a random number for the size of the salt.
                Random random = new Random();
                int saltSize = random.Next(minSaltSize, maxSaltSize);

                // Allocate a byte array, which will hold the salt.
                saltBytes = new byte[saltSize];

                // Initialize a random number generator.
                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

                // Fill the salt with cryptographically strong byte values.
                rng.GetNonZeroBytes(saltBytes);
            }

            // Convert plain text into a byte array.
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            // Allocate array, which will hold plain text and salt.
            byte[] plainTextWithSaltBytes =
                    new byte[plainTextBytes.Length + saltBytes.Length];

            // Copy plain text bytes into resulting array.
            for (int i = 0; i < plainTextBytes.Length; i++)
                plainTextWithSaltBytes[i] = plainTextBytes[i];

            // Append salt bytes to the resulting array.
            for (int i = 0; i < saltBytes.Length; i++)
                plainTextWithSaltBytes[plainTextBytes.Length + i] = saltBytes[i];

            // Because we support multiple hashing algorithms, we must define
            // hash object as a common (abstract) base class. We will specify the
            // actual hashing algorithm class later during object creation.
            HashAlgorithm hash;

           

            // Initialize appropriate hashing algorithm class.
            hash = new SHA512Managed();

            // Compute hash value of our plain text with appended salt.
            byte[] hashBytes = hash.ComputeHash(plainTextWithSaltBytes);

            // Create array which will hold hash and original salt bytes.
            byte[] hashWithSaltBytes = new byte[hashBytes.Length +
                                                saltBytes.Length];

            // Copy hash bytes into resulting array.
            for (int i = 0; i < hashBytes.Length; i++)
                hashWithSaltBytes[i] = hashBytes[i];

            // Append salt bytes to the result.
            for (int i = 0; i < saltBytes.Length; i++)
                hashWithSaltBytes[hashBytes.Length + i] = saltBytes[i];

            // Convert result into a base64-encoded string.
            string hashValue = Convert.ToBase64String(hashWithSaltBytes);

            // Return the result.
            return hashValue;
        }

        public bool VerifyHash(string plainText,
                                  string hashValue)
        {
            // Convert base64-encoded hash value into a byte array.
            byte[] hashWithSaltBytes = Convert.FromBase64String(hashValue);

            // We must know size of hash (without salt).
            int hashSizeInBits, hashSizeInBytes;


            // Size of hash is based on the specified algorithm.
            hashSizeInBits = 512;

            // Convert size of hash from bits to bytes.
            hashSizeInBytes = hashSizeInBits / 8;

            // Make sure that the specified hash value is long enough.
            if (hashWithSaltBytes.Length < hashSizeInBytes)
                return false;

            // Allocate array to hold original salt bytes retrieved from hash.
            byte[] saltBytes = new byte[hashWithSaltBytes.Length -
                                        hashSizeInBytes];

            // Copy salt from the end of the hash to the new array.
            for (int i = 0; i < saltBytes.Length; i++)
                saltBytes[i] = hashWithSaltBytes[hashSizeInBytes + i];

            // Compute a new hash string.
            string expectedHashString =
                        ComputeHash(plainText, saltBytes);

            // If the computed hash matches the specified hash,
            // the plain text value must be correct.
            return (hashValue == expectedHashString);
        }

        public async Task ChangePassword(string Email, string OldPassword, string NewPassword)
        {
            var mapper = new Mapper(mapConfig);

            var user = await unit.Repository<User>().Find(u => u.Email == Email);
            if (user != null)
            {
                if (VerifyHash(OldPassword, user.Password))
                {
                    user.Password = ComputeHash(NewPassword, null);
                    unit.Repository<User>().Update(mapper.Map<User>(user));
                    unit.Save();
                }
            }

        }
    }
}