<%@ Page Title="" Language="C#" MasterPageFile="HomeDefault.Master" AutoEventWireup="true" CodeBehind="TrangChu.aspx.cs" Inherits="DATN.WEBSITE.TrangChu" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web" Assembly="DevExpress.Web.v15.2, Version=15.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<asp:Content ID="Content1" runat="server" contentplaceholderid="ContentPlaceHolder1">
            <section id="intro" class="intro" style="font-family: Time New Roman">

            <div class="slogan">
                <h2>WELCOME TO HUFI </h2>
                <h4>TRƯỜNG ĐẠI HỌC CÔNG NGHIỆP THỰC PHẨM TP.HỒ CHÍ MINH<br />
                    PHÒNG ĐÀO TẠO
                </h4>
            </div>
            <div class="page-scroll">
                <a href="#service" class="btn btn-circle">
                    <i class="fa fa-angle-double-down animated"></i>
                </a>
            </div></section>
        <section id="service" class="home-section text-center bg-gray">
            <div class="heading-about">
                <div class="container">
                    <div class="row">
                        <div class="col-lg-8 col-lg-offset-2">
                            <div class="wow bounceInDown" data-wow-delay="0.4s">
                                <div class="section-heading">
                                    <h2>Dịch vụ</h2>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="container">
                <div class="row" style="margin-left: 180px">
                    <div class="col-sm-3 col-md-3">
                        <div class="wow fadeInUp" data-wow-delay="0.2s">
                            <div class="service-box">
                                <div class="service-icon">
                                    <img src="img/icons/service-icon-2.png" alt="" />
                                </div>
                                <div class="service-desc">
                                    <h5>Web Design</h5>
                                    <p>Mvc5, Asp.net, Angularis, Bootstrap, Wcf, WebApi</p>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-3 col-md-3">
                        <div class="wow fadeInUp" data-wow-delay="0.2s">
                            <div class="service-box">
                                <div class="service-icon">
                                    <img src="img/icons/outsource-large.jpg" alt="" width="30px" height="30px" />
                                </div>
                                <div class="service-desc">
                                    <h5>Outsourcing</h5>
                                    <p>We are team work</p>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-3 col-md-3">
                        <div class="wow fadeInRight" data-wow-delay="0.2s">
                            <div class="service-box">
                                <div class="service-icon">
                                    <img src="img/icons/service-icon-4.png" alt="" />
                                </div>
                                <div class="service-desc">
                                    <h5>Developer</h5>
                                    <p>Application C# for Windown, Java, Android, IOS,</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <div class="row" style="margin-left: 300px; font-family: Time New Roman; width: 2200px">
            <div class="col-lg-4">
                <div class="widget-contact">
                    <address>
                        <strong>Trường Đại Học Công Nghiệp Thực Phẩm</strong><br>
                        Khoa Công Nghệ Thông Tin -- Lớp 03DHTH1<br>
                        GVHD:Bùi Công Danh<br />
                        Nguyễn Minh Thông - Lê Vũ Triều<br />
                        <abbr title="Phone">Phone:</abbr>0976 809 907
                    </address>
                    <address>
                        <strong>Skype:ngminhthong19@live.com</strong><br />
                        <strong>Email:</strong><a href="mailto:#">ngminhthong.cntp@gmail.com</a><br/>
                        <strong>Facebook:</strong><a href="https://www.facebook.com/nguyenminhthong.developer" target="_blank">Nguyễn Minh Thông</a>
                    </address>
                </div>
            </div>
        </div>
        <script src="js/jquery.min.js" type="text/javascript"></script>
        <script src="js/bootstrap.min.js" type="text/javascript"></script>
        <script src="js/jquery.easing.min.js" type="text/javascript"></script>
        <script src="js/jquery.scrollTo.js" type="text/javascript"></script>
        <script src="js/wow.min.js"></script>
        <script src="js/custom.js" type="text/javascript"></script>
</asp:Content>

