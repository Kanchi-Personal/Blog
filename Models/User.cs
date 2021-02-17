using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

namespace BlogApi.Models
{
    public class User : BriefAuditBaseEntity
    {
        private string _username;
        private string _email;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string UserFullName { get; set; }

        [Required]
        [MaxLength(25)]
        public string Username
        {
            get => _username;
            set => _username = value.Trim().ToLower();
        }

        public byte[] PasswordHash { get; private set; }
        public byte[] PasswordSalt { get; private set; }

        [EmailAddress]
        [Required]
        [MaxLength(100)]
        public string Email
        {
            get => _email;
            set => _email = value.Trim().ToLower();
        }
        #region Methods
        public void SetPassword(string password)
        {
            using var hmac = new HMACSHA512();
            PasswordSalt = hmac.Key;
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        public bool ComparePassword(string password)
        {
            using var hmac = new HMACSHA512(PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.IsEquals(PasswordHash);
        }
        #endregion
    }
    public static class ArrayExtensions
    {
        public static bool IsEquals<T>(this T[] array1, T[] array2) where T : struct
        {
            if (array1.Length != array2.Length) return false;
            return !array1.Where((item1, item2) => !item1.Equals(array2[item2])).Any();
        }
    }
}
