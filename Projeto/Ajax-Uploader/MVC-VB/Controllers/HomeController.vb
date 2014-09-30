Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Mvc

Namespace MVCVB.Controllers
	<HandleError()> _
	 Public Class HomeController
		Inherits Controller
		Public Function Index() As ActionResult
			ViewData("Message") = "MVC File Upload like GMail"

			Return View()
		End Function
	End Class
End Namespace