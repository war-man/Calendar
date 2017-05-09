using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Calendar.Data;
using Calendar.Models;
using Calendar.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting.Server;
using System.Net;

namespace Calendar.Controllers
{
    public class AttachmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _environment;

        public IEnumerable<Attachment> ListAll()
        {
            return _context.Attachment.AsEnumerable();
        }

        //public AttachmentsController(ApplicationDbContext context)
        //{
        //    _context = context;
        //}

        public AttachmentsController(ApplicationDbContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<IActionResult> Index(int? eventid)
        {
            //var attachment = await _context.Attachment.SingleOrDefaultAsync(m => m.EventID == eventid);
            if (eventid == null)
            {
                return View(await _context.Attachment.OrderBy(m => m.EventID).ThenBy(m => m.ID).ToListAsync());
            }
            else
            {
                return View(await _context.Attachment.Where(m => m.EventID == eventid).OrderBy(m => m.EventID).ThenBy(m => m.ID).ToListAsync());
            }
        }

        // GET: Attachments 
        public async Task<IActionResult> IndexPartial(int eventid)
        {
            return PartialView("AttachPartial", await _context.Attachment.Where(m => m.EventID == eventid).ToListAsync());
        }


        // GET: Attachments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attachment = await _context.Attachment.SingleOrDefaultAsync(m => m.ID == id);
            if (attachment == null)
            {
                return NotFound();
            }

            return View(attachment);
        }

        // GET: Attachments/Create
        public IActionResult Create()
        {
            if (!User.IsInRole(Constants.ROLE_ADMIN))
                return NotFound();

            return View();
        }

        // POST: Attachments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,EventID,FileName,FilePath")] Attachment attachment)
        {

            if (!User.IsInRole(Constants.ROLE_ADMIN))
                return NotFound();

            if (ModelState.IsValid)
            {
                /* Audit Fields */
                var username = "anonymous";
                var u = User.Claims.Where(m => m.Type == "username");
                if (u.Count() == 1) { username = u.First().Value; }
                var displayname = "anonymous";
                var d = User.Claims.Where(m => m.Type == "displayName");
                if (d.Count() == 1) { displayname = d.First().Value; }
                attachment.CreatedDate = DateTime.Now;
                attachment.CreatedBy = username;
                attachment.CreatedByDisplayName = displayname;
                attachment.UpdatedDate = attachment.CreatedDate;
                attachment.UpdatedBy = username;
                attachment.UpdatedByDisplayName = displayname;

                _context.Add(attachment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(attachment);
        }

        // GET: Attachments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!User.IsInRole(Constants.ROLE_ADMIN))
                return NotFound();

            if (id == null)
            {
                return NotFound();
            }

            var attachment = await _context.Attachment.SingleOrDefaultAsync(m => m.ID == id);
            if (attachment == null)
            {
                return NotFound();
            }
            return View(attachment);
        }

        // POST: Attachments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,EventID,FileName,FilePath,CreatedDate,CreatedBy,CreatedByDisplayName")] Attachment attachment)
        {
            if (!User.IsInRole(Constants.ROLE_ADMIN))
                return NotFound();

            if (id != attachment.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    /* Audit Fields */
                    var username = "anonymous";
                    var u = User.Claims.Where(m => m.Type == "username");
                    if (u.Count() == 1) { username = u.First().Value; }
                    var displayname = "anonymous";
                    var d = User.Claims.Where(m => m.Type == "displayName");
                    if (d.Count() == 1) { displayname = d.First().Value; }
                    attachment.UpdatedDate = DateTime.Now;
                    attachment.UpdatedBy = username;
                    attachment.UpdatedByDisplayName = displayname;

                    _context.Update(attachment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttachmentExists(attachment.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", new { eventid = attachment.EventID });
            }
            return View(attachment);
        }

        // GET: Attachments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!User.IsInRole(Constants.ROLE_ADMIN))
                return NotFound();

            if (id == null)
            {
                return NotFound();
            }

            var attachment = await _context.Attachment.SingleOrDefaultAsync(m => m.ID == id);
            if (attachment == null)
            {
                return NotFound();
            }

            return View(attachment);
        }

        // POST: Attachments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, string ajax)
        {
            if (!User.IsInRole(Constants.ROLE_ADMIN))
                return NotFound();

            var attachment = await _context.Attachment.SingleOrDefaultAsync(m => m.ID == id);
            var uploads = Path.Combine(_environment.WebRootPath, "uploads", attachment.EventID.ToString(), attachment.ID.ToString());
            string fullPath = Path.Combine(uploads, attachment.FileName);
            try
            {
                if (System.IO.File.Exists(fullPath))
                {
                    //delete the file
                    System.IO.File.Delete(fullPath);

                    //delete the directory for file
                    System.IO.Directory.Delete(uploads);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            _context.Attachment.Remove(attachment);
            await _context.SaveChangesAsync();

            if (ajax == "true")
                return new EmptyResult();
            else
                return RedirectToAction("Index");

            //return RedirectToAction("Index", new { eventid = attachment.EventID });
        }

        private bool AttachmentExists(int id)
        {
            return _context.Attachment.Any(e => e.ID == id);
        }

        [HttpPost]
        public async Task<JsonResult> Upload2(ICollection<IFormFile> AttachFile, int EventID, string redir = null, string ajax = null)
        {
            try
            {
                var uploads = Path.Combine(_environment.WebRootPath, "uploads", EventID.ToString());
                var attachment = new Attachment();

                foreach (var file in AttachFile)
                {
                    if (file.Length > 0)
                    {
                        /* Audit Fields */
                        var username = "anonymous";
                        var u = User.Claims.Where(m => m.Type == "username");
                        if (u.Count() == 1) { username = u.First().Value; }
                        var displayname = "anonymous";
                        var d = User.Claims.Where(m => m.Type == "displayName");
                        if (d.Count() == 1) { displayname = d.First().Value; }
                        attachment.CreatedDate = DateTime.Now;
                        attachment.CreatedBy = username;
                        attachment.CreatedByDisplayName = displayname;
                        attachment.UpdatedDate = attachment.CreatedDate;
                        attachment.UpdatedBy = username;
                        attachment.UpdatedByDisplayName = displayname;
                        attachment.EventID = EventID;
                        attachment.FileName = Path.GetFileName(file.FileName);
                        attachment.FilePath = Path.Combine(uploads, Path.GetFileName(file.FileName));

                        _context.Add(attachment);
                        await _context.SaveChangesAsync();

                        /* Upload the file to path ~/uploads/eventid/attachmentid/attachmentname */
                        uploads = Path.Combine(_environment.WebRootPath, "uploads", EventID.ToString(), attachment.ID.ToString());
                        if (!Directory.Exists(uploads))
                        {
                            Directory.CreateDirectory(uploads);
                        }

                        using (var fileStream = new FileStream(Path.Combine(_environment.WebRootPath, uploads, Path.GetFileName(file.FileName)), FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                            ViewBag.UploadStatus = "Upload successfully.";

                            attachment.FilePath = Path.Combine(uploads, Path.GetFileName(file.FileName));
                            _context.Update(attachment);
                            await _context.SaveChangesAsync();
                        }
                    }
                }

                /*if (ajax == "true")
                    return new EmptyResult();
                else if (redir == "")
                    return RedirectToAction("Index");
                else
                {
                    ViewBag.Redir = redir;
                    if (!User.IsInRole(Constants.ROLE_ADMIN))
                        return NotFound();

                    RedirectToActionResult redirectResult = new RedirectToActionResult("Details", "Events", new { @id = attachment.EventID });
                    return redirectResult;
                }*/
            }
            catch (Exception)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Upload failed");
            }

            return Json("File uploaded successfully");
        }

        [HttpPost]
        public async Task<IActionResult> Upload(ICollection<IFormFile> AttachFile, int EventID, string redir = null, string ajax = null)
        {

            try
            {
                var uploads = Path.Combine(_environment.WebRootPath, "uploads", EventID.ToString());

                foreach (var file in AttachFile)
                {
                    if (file.Length > 0)
                    {
                        var attachment = new Attachment();
                        /* Audit Fields */
                        var username = "anonymous";
                        var u = User.Claims.Where(m => m.Type == "username");
                        if (u.Count() == 1) { username = u.First().Value; }
                        var displayname = "anonymous";
                        var d = User.Claims.Where(m => m.Type == "displayName");
                        if (d.Count() == 1) { displayname = d.First().Value; }
                        attachment.CreatedDate = DateTime.Now;
                        attachment.CreatedBy = username;
                        attachment.CreatedByDisplayName = displayname;
                        attachment.UpdatedDate = attachment.CreatedDate;
                        attachment.UpdatedBy = username;
                        attachment.UpdatedByDisplayName = displayname;
                        attachment.EventID = EventID;
                        attachment.FileName = Path.GetFileName(file.FileName);
                        attachment.FilePath = Path.Combine(uploads, Path.GetFileName(file.FileName));

                        _context.Add(attachment);
                        await _context.SaveChangesAsync();

                        /* Upload the file to path ~/uploads/eventid/attachmentid/attachmentname */
                        uploads = Path.Combine(_environment.WebRootPath, "uploads", EventID.ToString(), attachment.ID.ToString());
                        if (!Directory.Exists(uploads))
                        {
                            Directory.CreateDirectory(uploads);
                        }

                        using (var fileStream = new FileStream(Path.Combine(_environment.WebRootPath, uploads, Path.GetFileName(file.FileName)), FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                            ViewBag.UploadStatus = "Upload successfully.";

                            attachment.FilePath = Path.Combine(uploads, Path.GetFileName(file.FileName));
                            _context.Update(attachment);
                            await _context.SaveChangesAsync();
                        }
                    }
                }

                if (ajax == "true")
                    return new EmptyResult();
                else if (redir == "")
                    return RedirectToAction("Index");
                else
                {
                    ViewBag.Redir = redir;
                    if (!User.IsInRole(Constants.ROLE_ADMIN))
                        return NotFound();

                    RedirectToActionResult redirectResult = new RedirectToActionResult("Details", "Events", new { @id = EventID });
                    return redirectResult;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + "<br>" + e.InnerException.Message);
            }
        }

        //[HttpPost]
        public FileResult Download(int id)
        {
            var attachment = _context.Attachment.SingleOrDefaultAsync(m => m.ID == id);
            if (attachment == null)
            {
                return null;
            }
            else
            {
                string fullName = attachment.Result.FilePath;
                string extension = Path.GetExtension(attachment.Result.FileName);

                byte[] fileBytes = GetFile(fullName);
                return File(
                    fileBytes, ReturnExtension(extension), attachment.Result.FileName);
            }
        }

        byte[] GetFile(string s)
        {
            System.IO.FileStream fs = System.IO.File.OpenRead(s);
            byte[] data = new byte[fs.Length];
            int br = fs.Read(data, 0, data.Length);
            if (br != fs.Length)
                throw new System.IO.IOException(s);
            return data;
        }

        private string ReturnExtension(string fileExtension)
        {
            switch (fileExtension)
            {
                case ".htm":
                case ".html":
                case ".log":
                    return "text/HTML";
                case ".txt":
                    return "text/plain";
                case ".doc":
                    return "application/ms-word";
                case ".tiff":
                case ".tif":
                    return "image/tiff";
                case ".asf":
                    return "video/x-ms-asf";
                case ".avi":
                    return "video/avi";
                case ".zip":
                    return "application/zip";
                case ".xls":
                case ".csv":
                    return "application/vnd.ms-excel";
                case ".gif":
                    return "image/gif";
                case ".jpg":
                case "jpeg":
                    return "image/jpeg";
                case ".bmp":
                    return "image/bmp";
                case ".wav":
                    return "audio/wav";
                case ".mp3":
                    return "audio/mpeg3";
                case ".mpg":
                case "mpeg":
                    return "video/mpeg";
                case ".rtf":
                    return "application/rtf";
                case ".asp":
                    return "text/asp";
                case ".pdf":
                    return "application/pdf";
                case ".fdf":
                    return "application/vnd.fdf";
                case ".ppt":
                    return "application/mspowerpoint";
                case ".dwg":
                    return "image/vnd.dwg";
                case ".msg":
                    return "application/msoutlook";
                case ".xml":
                case ".sdxl":
                    return "application/xml";
                case ".xdp":
                    return "application/vnd.adobe.xdp+xml";
                default:
                    return "application/octet-stream";
            }

        }
    }
}