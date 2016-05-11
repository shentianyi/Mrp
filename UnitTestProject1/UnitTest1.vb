Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass()> Public Class UnitTest1

    <TestMethod()> Public Sub TestListSort()
        Dim li As List(Of ObjectWithTime) = New List(Of ObjectWithTime)
        li.Add(New ObjectWithTime With {.ObjId = "1", .ObjTime = New DateTime(2015, 3, 1, 12, 30, 10)})
        li.Add(New ObjectWithTime With {.ObjId = "2", .ObjTime = New DateTime(2012, 3, 1, 12, 30, 10)})
        li.Add(New ObjectWithTime With {.ObjId = "3", .ObjTime = New DateTime(2014, 3, 1, 12, 30, 10)})
        li.Add(New ObjectWithTime With {.ObjId = "4", .ObjTime = New DateTime(2011, 3, 1, 12, 30, 10)})
        li.Sort(New Comparison(Of ObjectWithTime)(Function(x, y) x.ObjTime < y.ObjTime))
        Assert.IsTrue(li(0).ObjId = "4")
        Assert.IsTrue(li(1).ObjId = "2")
        Assert.IsTrue(li(2).ObjId = "3")
        Assert.IsTrue(li(3).ObjId = "1")
    End Sub

    <TestMethod> Public Sub TestGetRidOfNull()
        Dim li As List(Of ObjectWithTime) = New List(Of ObjectWithTime)
        li.Add(New ObjectWithTime)
        li.Add(New ObjectWithTime)
        li.Add(Nothing)
        li.Add(Nothing)

        li.RemoveAll(Function(c) c Is Nothing)
        Assert.IsTrue(li.Count = 2)
        For Each liele As ObjectWithTime In li
            Assert.IsNotNull(liele)
        Next
    End Sub

End Class

Public Class ObjectWithTime
    Private _objId As String
    Private _objTime As DateTime
    Public Property ObjId As String
        Get
            Return _objId
        End Get
        Set(value As String)
            _objId = value
        End Set
    End Property

    Public Property ObjTime As DateTime
        Get
            Return _objTime
        End Get
        Set(value As DateTime)
            _objTime = value
        End Set
    End Property

End Class