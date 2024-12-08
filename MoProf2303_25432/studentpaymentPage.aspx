<%@ Page Title="" Language="C#" MasterPageFile="~/StudentMasterPage.Master" AutoEventWireup="true" CodeBehind="studentpaymentPage.aspx.cs" Inherits="MoProf2303_25432.studentpaymentPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
    <style>
        .main {
            background-color: #f0f4f8;
            min-height: 100vh;
            display: flex;
            flex-direction: column;
            align-items: center;
            padding: 2rem;
        }

        .payment-form-container {
            display: flex;
            flex-direction: column;
            justify-content: center;
            border-radius: 12px;
            padding: 2rem;
            background-color: #ffffff;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            width: 100%;
            max-width: 600px;
            margin: 2rem auto;
        }

        .payment-form-container h1 {
            font-size: 2rem;
            color: #333;
            margin-bottom: 1rem;
        }

        .payment-form-container label {
            margin-top: 1rem;
            font-weight: bold;
            font-size: 1rem;
            color: #333;
        }

        .payment-form-container input,
        .payment-form-container select {
            border: 1px solid #ccc;
            background: transparent;
            height: 2.5rem;
            width: 100%;
            padding: 0 0.75rem;
            margin-top: 0.5rem;
            border-radius: 8px;
            font-size: 1rem;
            margin-bottom: 1rem;
        }

        .payment-form-container .btn {
            background: linear-gradient(135deg, #007bff, #0056b3);
            border: none;
            border-radius: 8px;
            padding: 0.5rem 1rem;
            color: #fff;
            cursor: pointer;
            font-size: 1rem;
            transition: all 0.3s;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        }

        .payment-form-container .btn:hover {
            background: linear-gradient(135deg, #0056b3, #003f7f);
            box-shadow: 0 6px 12px rgba(0, 0, 0, 0.2);
        }

        .text-center {
            text-align: center;
        }

        .text-primary {
            color: #007bff;
        }

        .error-message {
            color: red;
            font-size: 0.875rem;
            margin-top: -0.5rem;
            margin-bottom: 1rem;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <main class="main">
        <div class="payment-form-container">
            <h1>Payment Form</h1>
            <asp:Label ID="lblCourseName" runat="server" Text=""></asp:Label>
            <asp:Label ID="lblTutorName" runat="server" Text=""></asp:Label>
            <asp:Label ID="lblFees" runat="server" Text=""></asp:Label>

            <asp:Label ID="lblCardNumber" runat="server" Text="Card Number"></asp:Label>
            <asp:TextBox ID="txtCardNumber" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvCardNumber" runat="server" ControlToValidate="txtCardNumber" ErrorMessage="Card Number is required." CssClass="error-message" Display="Dynamic" />

            <asp:Label ID="lblExpirationDate" runat="server" Text="Expiration Date"></asp:Label>
            <asp:TextBox ID="txtExpirationDate" runat="server" CssClass="form-control" Placeholder="MM/YY"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvExpirationDate" runat="server" ControlToValidate="txtExpirationDate" ErrorMessage="Expiration Date is required." CssClass="error-message" Display="Dynamic" />

            <asp:Label ID="lblCVV" runat="server" Text="CVV"></asp:Label>
            <asp:TextBox ID="txtCVV" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvCVV" runat="server" ControlToValidate="txtCVV" ErrorMessage="CVV is required." CssClass="error-message" Display="Dynamic" />

            <asp:Button ID="btnSubmitPayment" runat="server" Text="Submit Payment" CssClass="btn" OnClick="btnSubmitPayment_Click" />
        </div>
    </main>
</asp:Content>