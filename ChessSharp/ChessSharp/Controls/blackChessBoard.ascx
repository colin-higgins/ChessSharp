<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="blackChessBoard.ascx.cs" Inherits="ChessSharp.blackChessBoard" %>



<asp:Panel ID="boardPanel" runat="server">
        <asp:Table ID="blackChessTable" CssClass="chess" runat="server">

            <asp:TableRow ID="boardrow1" runat="server">
				<asp:TableCell id="square00" CssClass="square" runat="server"></asp:TableCell>
                <asp:TableCell id="square01" CssClass="square" runat="server"></asp:TableCell>
                <asp:TableCell id="square02" CssClass="square" runat="server"></asp:TableCell>
                <asp:TableCell id="square03" CssClass="square" runat="server"></asp:TableCell>
                <asp:TableCell id="square04" CssClass="square" runat="server"></asp:TableCell>
                <asp:TableCell id="square05" CssClass="square" runat="server"></asp:TableCell>
                <asp:TableCell id="square06" CssClass="square" runat="server"></asp:TableCell>
                <asp:TableCell id="square07" CssClass="square" runat="server"></asp:TableCell>
            </asp:TableRow>  
            <asp:TableRow  ID="boardrow2" runat="server">
				<asp:TableCell id="square08" CssClass="square" runat="server"></asp:TableCell>
                <asp:TableCell id="square09" CssClass="square" runat="server"></asp:TableCell>
                <asp:TableCell id="square10" CssClass="square" runat="server"></asp:TableCell>
                <asp:TableCell id="square11" CssClass="square" runat="server"></asp:TableCell>
                <asp:TableCell id="square12" CssClass="square" runat="server"></asp:TableCell>
                <asp:TableCell id="square13" CssClass="square" runat="server"></asp:TableCell>
                <asp:TableCell id="square14" CssClass="square" runat="server"></asp:TableCell>
                <asp:TableCell id="square15" CssClass="square" runat="server"></asp:TableCell>
            </asp:TableRow>             
            <asp:TableRow  ID="boardrow3" runat="server">
				<asp:TableCell id="square16" CssClass="square" runat="server"></asp:TableCell>
                <asp:TableCell id="square17" CssClass="square" runat="server"></asp:TableCell>
                <asp:TableCell id="square18" CssClass="square" runat="server"></asp:TableCell>
                <asp:TableCell id="square19" CssClass="square" runat="server"></asp:TableCell>
                <asp:TableCell id="square20" CssClass="square" runat="server"></asp:TableCell>
                <asp:TableCell id="square21" CssClass="square" runat="server"></asp:TableCell>
                <asp:TableCell id="square22" CssClass="square" runat="server"></asp:TableCell>
                <asp:TableCell id="square23" CssClass="square" runat="server"></asp:TableCell>
            </asp:TableRow>           
            <asp:TableRow  ID="boardrow4" runat="server">
				<asp:TableCell id="square24" CssClass="square" runat="server"></asp:TableCell>
                <asp:TableCell id="square25" CssClass="square" runat="server"></asp:TableCell>
                <asp:TableCell id="square26" CssClass="square" runat="server"></asp:TableCell>
                <asp:TableCell id="square27" CssClass="square" runat="server"></asp:TableCell>
                <asp:TableCell id="square28" CssClass="square" runat="server"></asp:TableCell>
                <asp:TableCell id="square29" CssClass="square" runat="server"></asp:TableCell>
                <asp:TableCell id="square30" CssClass="square" runat="server"></asp:TableCell>
                <asp:TableCell id="square31" CssClass="square" runat="server"></asp:TableCell>
            </asp:TableRow>            
            <asp:TableRow  ID="boardrow5" runat="server">
				<asp:TableCell id="square32" CssClass="square" runat="server"></asp:TableCell>
                <asp:TableCell id="square33" CssClass="square" runat="server"></asp:TableCell>
                <asp:TableCell id="square34" CssClass="square" runat="server"></asp:TableCell>
                <asp:TableCell id="square35" CssClass="square" runat="server"></asp:TableCell>
                <asp:TableCell id="square36" CssClass="square" runat="server"></asp:TableCell>
                <asp:TableCell id="square37" CssClass="square" runat="server"></asp:TableCell>
                <asp:TableCell id="square38" CssClass="square" runat="server"></asp:TableCell>
                <asp:TableCell id="square39" CssClass="square" runat="server"></asp:TableCell>
            </asp:TableRow>             
            <asp:TableRow  ID="boardrow6" runat="server">
				<asp:TableCell id="square40" CssClass="square" runat="server"></asp:TableCell>
                <asp:TableCell id="square41" CssClass="square" runat="server"></asp:TableCell>
                <asp:TableCell id="square42" CssClass="square" runat="server"></asp:TableCell>
                <asp:TableCell id="square43" CssClass="square" runat="server"></asp:TableCell>
                <asp:TableCell id="square44" CssClass="square" runat="server"></asp:TableCell>
                <asp:TableCell id="square45" CssClass="square" runat="server"></asp:TableCell>
                <asp:TableCell id="square46" CssClass="square" runat="server"></asp:TableCell>
                <asp:TableCell id="square47" CssClass="square" runat="server"></asp:TableCell>
            </asp:TableRow>           
            <asp:TableRow  ID="boardrow7" runat="server">
				<asp:TableCell id="square48" CssClass="square" runat="server"></asp:TableCell>
                <asp:TableCell id="square49" CssClass="square" runat="server"></asp:TableCell>
                <asp:TableCell id="square50" CssClass="square" runat="server"></asp:TableCell>
                <asp:TableCell id="square51" CssClass="square" runat="server"></asp:TableCell>
                <asp:TableCell id="square52" CssClass="square" runat="server"></asp:TableCell>
                <asp:TableCell id="square53" CssClass="square" runat="server"></asp:TableCell>
                <asp:TableCell id="square54" CssClass="square" runat="server"></asp:TableCell>
                <asp:TableCell id="square55" CssClass="square" runat="server"></asp:TableCell>           
            </asp:TableRow>           
            <asp:TableRow  ID="boardrow8" runat="server">
				<asp:TableCell id="square56" CssClass="square" runat="server"></asp:TableCell>
                <asp:TableCell id="square57" CssClass="square" runat="server"></asp:TableCell>
                <asp:TableCell id="square58" CssClass="square" runat="server"></asp:TableCell>
                <asp:TableCell id="square59" CssClass="square" runat="server"></asp:TableCell>
                <asp:TableCell id="square60" CssClass="square" runat="server"></asp:TableCell>
                <asp:TableCell id="square61" CssClass="square" runat="server"></asp:TableCell>
                <asp:TableCell id="square62" CssClass="square" runat="server"></asp:TableCell>
                <asp:TableCell id="square63" CssClass="square" runat="server"></asp:TableCell>
            </asp:TableRow>

        </asp:Table>

        

    </asp:Panel>