using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalSite.Core.Interfaces;
using PersonalSite.Web.Areas.Admin.Models;

namespace PersonalSite.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "SuperAdmin")]
public class UsersController : Controller
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var users = await _userService.GetAllUsersAsync();
        return View(users);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null)
            return NotFound();

        return View(user);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new UserEditViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(UserEditViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // Validate password is provided
        if (string.IsNullOrEmpty(model.Password))
        {
            ModelState.AddModelError("Password", "Password is required.");
            return View(model);
        }

        // Check if username or email already exists
        var existingUser = await _userService.GetUserByUsernameAsync(model.Username);
        if (existingUser != null)
        {
            ModelState.AddModelError("Username", "Username already exists.");
            return View(model);
        }

        existingUser = await _userService.GetUserByEmailAsync(model.Email);
        if (existingUser != null)
        {
            ModelState.AddModelError("Email", "Email already exists.");
            return View(model);
        }

        var user = new Core.Models.User
        {
            Username = model.Username,
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Role = model.Role,
            IsActive = model.IsActive
        };

        await _userService.CreateUserAsync(user, model.Password);
        TempData["SuccessMessage"] = "User successfully created!";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        var model = new UserEditViewModel
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Role = user.Role,
            IsActive = user.IsActive
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(UserEditViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await _userService.GetUserByIdAsync(model.Id);
        if (user == null)
        {
            return NotFound();
        }

        // Check if username or email already exists (excluding current user)
        var existingUser = await _userService.GetUserByUsernameAsync(model.Username);
        if (existingUser != null && existingUser.Id != model.Id)
        {
            ModelState.AddModelError("Username", "Username already exists.");
            return View(model);
        }

        existingUser = await _userService.GetUserByEmailAsync(model.Email);
        if (existingUser != null && existingUser.Id != model.Id)
        {
            ModelState.AddModelError("Email", "Email already exists.");
            return View(model);
        }

        user.Username = model.Username;
        user.Email = model.Email;
        user.FirstName = model.FirstName;
        user.LastName = model.LastName;
        user.Role = model.Role;
        user.IsActive = model.IsActive;

        await _userService.UpdateUserAsync(user);
        TempData["SuccessMessage"] = "User successfully updated!";
        return RedirectToAction(nameof(Details), new { id = user.Id });
    }

    [HttpGet]
    public async Task<IActionResult> ChangePassword(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        var viewModel = new ChangePasswordViewModel
        {
            UserId = user.Id,
            Username = user.Username
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var result = await _userService.ChangePasswordAsync(model.UserId, model.CurrentPassword, model.NewPassword);
        
        if (!result)
        {
            ModelState.AddModelError("CurrentPassword", "Current password is incorrect.");
            return View(model);
        }

        TempData["SuccessMessage"] = "Password successfully changed!";
        return RedirectToAction(nameof(Details), new { id = model.UserId });
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        return View(user);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _userService.DeleteUserAsync(id);
        TempData["SuccessMessage"] = "User successfully deleted!";
        return RedirectToAction(nameof(Index));
    }
}