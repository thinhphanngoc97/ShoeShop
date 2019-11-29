using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.DAO;
using ShoeShop.Common;
using System.Drawing;
using System.IO;

namespace ShoeShop.Areas.Admin.Controllers
{
    public class ProductController : BaseController
	{
        ProductDAO productDAO = new ProductDAO();

        public ActionResult Product()
        {
            var model = productDAO.GetAllProducts();

            return View(model);
        }

        [HttpGet]
        public ActionResult ProductDetail(int id, string meta_name)
        {
            var model = productDAO.GetProductById(id);

            ViewBag.ListCategory = new SelectList(new CategoryDAO().GetListOfCategories(), "Id", "Name", model.CATEGORY.Name);
            ViewBag.ListManufacturer = new SelectList(new ManufacturerDAO().GetAllManufacturers(), "Id", "Name", model.MANUFACTURER.Name);

            return View(model);
        }

        [HttpPost]
        public ActionResult EditProduct(PRODUCT product, IEnumerable<HttpPostedFileBase> other_imgs)
        {
            try
            {
                product.Metadata = Common.MetaTitleCreator.CreateMetaTitle(product.Name);
                productDAO.UpdateProduct(product);

                if (other_imgs.First() != null)
                {
                    productDAO.RemoveAllImagesByProductId(product.Id);

                    foreach (var item in other_imgs)
                    {
                        Stream fs = item.InputStream;
                        BinaryReader br = new BinaryReader(fs);

                        byte[] bytes = br.ReadBytes((int)fs.Length);
                        string extension = item.FileName.Split('.').Last().ToLower();

                        if (string.Equals(extension, "jpg"))
                        {
                            extension = "jpeg";
                        }

                        string base64String = "data:image/" + extension + ";base64," + Convert.ToBase64String(bytes, 0, bytes.Length);

                        PRODUCT_IMAGE productImage = new PRODUCT_IMAGE
                        {
                            Id_Product = product.Id,
                            Data = base64String
                        };

                        productDAO.InsertProductImage(productImage);
                    }
                }
                return RedirectToAction("Product");
            }
            catch (Exception)
            {
                return RedirectToAction("ProductDetail", new
                {
                    @id = product.Id,
                    @meta_name = product.Metadata
                });
            }
        }

        [HttpGet]
        public ActionResult AddProduct()
        {
            ViewBag.ListCategory = new SelectList(new CategoryDAO().GetListOfCategories(), "Id", "Name");
            ViewBag.ListManufacturer = new SelectList(new ManufacturerDAO().GetAllManufacturers(), "Id", "Name");

            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(PRODUCT product, IEnumerable<HttpPostedFileBase> other_imgs)
        {
            try
            {
                product.Metadata = Common.MetaTitleCreator.CreateMetaTitle(product.Name);
                product.Created_Time = DateTime.Now;

                productDAO.InsertProduct(product);

                if (other_imgs.First() != null)
                {
                    productDAO.RemoveAllImagesByProductId(product.Id);

                    foreach (var item in other_imgs)
                    {
                        Stream fs = item.InputStream;
                        BinaryReader br = new BinaryReader(fs);

                        byte[] bytes = br.ReadBytes((int)fs.Length);
                        string extension = item.FileName.Split('.').Last().ToLower();

                        if (string.Equals(extension, "jpg"))
                        {
                            extension = "jpeg";
                        }

                        string base64String = "data:image/" + extension + ";base64," + Convert.ToBase64String(bytes, 0, bytes.Length);

                        PRODUCT_IMAGE productImage = new PRODUCT_IMAGE
                        {
                            Id_Product = product.Id,
                            Data = base64String
                        };

                        productDAO.InsertProductImage(productImage);
                    }
                }
                return RedirectToAction("Product");
            }
            catch (Exception)
            {
                return RedirectToAction("AddProduct");
            }
        }
        [HttpPost]
        public ActionResult DeleteProduct(int id)
        {
            return Json(new { result = productDAO.DeleteProduct(id) });
        }
    }
}