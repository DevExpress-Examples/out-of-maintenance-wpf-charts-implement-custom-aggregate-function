Imports DataAggregationSample.Model
Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Xml.Linq

Namespace DataAggregationSample.ViewModel

    Friend Class MainViewModel

        Private _CurrencyRates As IReadOnlyList(Of DataAggregationSample.Model.CurrencyRate)

        Const DataFilePath As [String] = "..//..//Data//EurUsdRate.xml"

        Public Property CurrencyRates As IReadOnlyList(Of CurrencyRate)
            Get
                Return _CurrencyRates
            End Get

            Private Set(ByVal value As IReadOnlyList(Of CurrencyRate))
                _CurrencyRates = value
            End Set
        End Property

        Public Sub New()
            CurrencyRates = LoadRates()
        End Sub

        Const RootName As [String] = "EurUsd"

        Const ListName As [String] = "EurUsdRate"

        Const ItemName As [String] = "CurrencyRate"

        Const ItemDateName As [String] = "DateTime"

        Const ItemValueName As [String] = "Rate"

        Public Function LoadRates() As IReadOnlyList(Of CurrencyRate)
            Dim rates = New List(Of CurrencyRate)()
            Dim document = XDocument.Load(DataFilePath)
            Dim itemList = document.Element(RootName).Element(ListName)
            For Each item In itemList.Elements(ItemName)
                Dim [date] = DateTime.ParseExact(item.Element(ItemDateName).Value, "yyyy-MM-dd", CultureInfo.InvariantCulture)
                Dim rate = [Double].Parse(item.Element(ItemValueName).Value, CultureInfo.InvariantCulture)
                rates.Add(New CurrencyRate([date], rate))
            Next

            Return rates
        End Function
    End Class
End Namespace
