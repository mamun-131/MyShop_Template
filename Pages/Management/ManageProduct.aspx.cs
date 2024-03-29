﻿using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Management_ManageProduct : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetImages();

            if (!string.IsNullOrWhiteSpace(Request.QueryString["id"]))
            {
                int id = Convert.ToInt32(Request.QueryString["id"]);
                FillPage(id);
            }
        }

    }
    protected void Page_PreRender(object sender, EventArgs e)
    {

        // TreeView treeView = (TreeView)Master.FindControl("TreeView1");
        // treeView.Visible = false;
       // Panel panel = (Panel)Master.FindControl("pnlTree");
       // panel.Visible = false;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        ProductModel productModel = new ProductModel();
        Product product = CreateProduct();

        if (!string.IsNullOrWhiteSpace(Request.QueryString["id"]))
        {
            int id = Convert.ToInt32(Request.QueryString["id"]);
            lblResult.Text = productModel.UpdateProduct(id, product);

        }
            else
        {

            lblResult.Text = productModel.InsertProduct(product);
        }
        

    }

    private void FillPage(int id)
    {
        ProductModel productModel = new ProductModel();
        Product product = productModel.GetProduct(id);

        txtDescription.Text = product.Description;
        txtName.Text = product.Name;
        txtPrice.Text = product.Price.ToString();
        ddImage.SelectedValue = product.Image;
        ddType.SelectedValue = product.TypeId.ToString();

    }

    private void GetImages()
    {
        try
        {
            string[] images = Directory.GetFiles(Server.MapPath("~/Img/Products/"));
            ArrayList imageList = new ArrayList();
            foreach (string image in images)
            {
                string imageName = image.Substring(image.LastIndexOf(@"\", StringComparison.Ordinal) + 1);
                imageList.Add(imageName);                
            }
            ddImage.DataSource = imageList;
            ddImage.AppendDataBoundItems = true;
            ddImage.DataBind();
        }
        catch (Exception e)
        {
             lblResult.Text = e.ToString();
        }
        
    }

    private Product CreateProduct()
    {
        Product product = new Product();
        product.Name = txtName.Text;
        product.Price = Convert.ToInt32(txtPrice.Text);
        product.TypeId = Convert.ToInt32(ddType.SelectedValue);
        product.Description = txtDescription.Text;
        product.Image = ddImage.SelectedValue;
        return product;
    }
    
}