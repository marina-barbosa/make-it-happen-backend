// Path: Repositories/UserRepository.cs
// using make_it_happen.Context;
// using make_it_happen.Models;
// using Microsoft.EntityFrameworkCore;
// using System.Collections.Generic;
// using System.Linq;

// namespace make_it_happen.Repositories
// {
//     public class UserRepository(AppDbContext context) : IUserRepository
//     {
//         private readonly AppDbContext _context = context;

//     public IEnumerable<User> ListUsers()
//         {
//             return _context.Users!.AsNoTracking().ToList();
//         }

//         public User? GetUserById(int id)
//         {
//             return _context.Users!.AsNoTracking().FirstOrDefault(x => x.UserId == id);
//         }

//         public void CreateUser(User user)
//         {
//             _context.Users!.Add(user);
//             _context.SaveChanges();
//         }

//         public void UpdateUser(User user)
//         {
//             _context.Users!.Update(user);
//             _context.SaveChanges();
//         }

//         public void DeleteUser(int id)
//         {
//             var user = _context.Users!.FirstOrDefault(x => x.UserId == id);
//             if (user != null)
//             {
//                 _context.Users!.Remove(user);
//                 _context.SaveChanges();
//             }
//         }
//     }
// }
