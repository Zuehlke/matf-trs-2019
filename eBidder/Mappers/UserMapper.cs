using eBidder.Domain;
using eBidder.Models;

namespace eBidder.Mappers
{
    public static class UserMapper
    {
        public static User ToUser(this UserViewModel viewModel)
        {
            return new User
            {
                Username = viewModel.Username,
                Password = viewModel.Password
            };
        }

        public static UserViewModel ToUserViewModel(this User user)
        {
            return new UserViewModel
            {
                Username = user.Username,
                Password = user.Password,
                ConfirmPassword = user.Password
            };
        }
    }
}