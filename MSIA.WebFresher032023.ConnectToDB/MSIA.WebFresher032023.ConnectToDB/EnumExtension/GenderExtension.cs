using MSIA.WebFresher032023.ConnectToDB.Enum;

namespace MSIA.WebFresher032023.ConnectToDB.EnumExtension
{
    /// <summary>
    /// Cung cấp các phương thức mở rộng cho enum Gender.
    /// </summary>
    public static class GenderExtension
    {
        /// <summary>
        /// Chuyển đổi giá trị enum Gender thành chuỗi tương ứng.
        /// </summary>
        /// <param name="gender">Giá trị enum cần chuyển đổi.</param>
        /// <returns>Chuỗi tương ứng với giá trị Gender.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Ném ra giá trị khi gender không hợp lệ.</exception>
        public static string ConvertEnumGender(this Gender gender)
        {
            switch (gender)
            {
                case Gender.Male:
                    return "Male";
                case Gender.Female:
                    return "Female";
                case Gender.Other:
                    return "Other";
                default:
                    throw new ArgumentOutOfRangeException(nameof(gender), gender, "Invalid gender value");
            }
        }
        /// <summary>
        /// Chuyển đổi chuỗi thành giá trị enum Gender tương ứng.
        /// </summary>
        /// <param name="gender">Chuỗi gender cần chuyển đổi.</param>
        /// <returns>Giá trị enum Gender tương ứng với chuỗi.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Ném ra khi chuỗi gender không hợp lệ.</exception>
        public static int ConvertStringGender(this string gender)
        {
            switch (gender)
            {
                case "male":
                    return (int)Gender.Male;
                case "female":
                    return (int)Gender.Female;
                case "other":
                    return (int)Gender.Other;
                default:
                    throw new ArgumentOutOfRangeException(nameof(gender), gender, "Invalid gender value");
            }
        }
    }
}
