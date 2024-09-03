using Microsoft.AspNetCore.Mvc;
using Test.Models;
using Test.Repositories;

namespace Test.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactRepository _contactRepository;

        public ContactController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<IActionResult> Index()
        {
            var contacts = await _contactRepository.GetAllContactsAsync();
            return View(contacts);
        }

        [HttpGet]
        public IActionResult Create() => PartialView("_Create");

        [HttpPost]
        public async Task<IActionResult> Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                await _contactRepository.AddContactAsync(contact);
                return RedirectToAction("Index");
            }
            return PartialView("_Create", contact);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var contact = await _contactRepository.GetContactByIdAsync(id);
            return PartialView("_Edit", contact);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Contact contact)
        {
            if (ModelState.IsValid)
            {
                await _contactRepository.UpdateContactAsync(contact);
                return RedirectToAction("Index");
            }
            return PartialView("_Edit", contact);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _contactRepository.DeleteContactAsync(id);
            return RedirectToAction("Index");
        }
    }

}
