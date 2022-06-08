// ignore_for_file: file_names
import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_barcode_scanner/flutter_barcode_scanner.dart';
import 'package:nsretail/Screens/stockCountingList.dart';
import 'package:nsretail/api/items.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:http/http.dart' as http;

class StockItem extends StatefulWidget {
  int branchId;
  int nStockcountingID;

  StockItem(this.branchId, this.nStockcountingID);

  @override
  _StockItemState createState() => _StockItemState();
}

class _StockItemState extends State<StockItem> {
  bool isTextVisible = false;
  bool isDropddownVisible = false;
  String _errorMessage = "";
  int itemPriceId;
  String _scanBarcode = 'Unknown';
  List<Items> _itemsList = List<Items>();
  int _itemcodeid;
  double dbMRP;
  TextEditingController itemCodeController = new TextEditingController();
  TextEditingController itemNameController = new TextEditingController();
  TextEditingController MRPController = new TextEditingController();
  TextEditingController salesPriceController = new TextEditingController();
  TextEditingController QuantityController = new TextEditingController();

  final FocusNode _itemCodefocus = FocusNode();
  final FocusNode _itemNamefocus = FocusNode();
  final FocusNode _MRPfocus = FocusNode();
  final FocusNode _salesPricefocus = FocusNode();
  final FocusNode _Quantityfocus = FocusNode();

  final FocusNode _submitfocus = FocusNode();
  void onChange() {
    _scanBarcode = itemCodeController.text;
    setState(() {
      getData();
    });
    print('itemslist count');
    print(_itemsList.length);
    if (_itemsList.length > 1) {
      print(_scanBarcode + 'morethan 1');
      itemCodeController.text = _scanBarcode;
      String itemName = _itemsList[0].itemName;
      print(itemName);
      itemPriceId = _itemsList[0].itemPriceId;
      String salesPrice = _itemsList[0].salesPrice.toString();
      itemNameController.text = itemName;
      setState(() {
        MRP();
        isDropddownVisible = true;
        isTextVisible = false;
      });
      _fieldFocusChange(context, _itemCodefocus, _MRPfocus);
      //salesPriceController.text = salesPrice;
      _errorMessage = "";
    }
    if (_itemsList.length == 1) {
      print('1 record');
      itemCodeController.text = _scanBarcode;
      String itemName = _itemsList[0].itemName;
      print(itemName);
      itemPriceId = _itemsList[0].itemPriceId;

      String salesPrice = _itemsList[0].salesPrice.toString();
      itemNameController.text = itemName;
      MRPController.text = _itemsList[0].MRP.toString();
      setState(() {
        isDropddownVisible = false;
        isTextVisible = true;
      });

      salesPriceController.text = salesPrice;
      _errorMessage = "";
      _fieldFocusChange(context, _itemCodefocus, _Quantityfocus);
    } else {
      _errorMessage = "Item details not found";
      //clearFields();
    }
  }

  @override
  void initState() {
    // TODO: implement initState
    getData();
    itemCodeController.addListener(onChange);
    super.initState();

    //this.getData();
  }

  Future<void> startBarcodeScanStream() async {
    FlutterBarcodeScanner.getBarcodeStreamReceiver(
            '#ff6666', 'Cancel', true, ScanMode.BARCODE)
        .listen((barcode) => print(barcode));
  }

  Future<void> scanQR() async {
    String barcodeScanRes;
    // Platform messages may fail, so we use a try/catch PlatformException.
    try {
      barcodeScanRes = await FlutterBarcodeScanner.scanBarcode(
          '#ff6666', 'Cancel', true, ScanMode.QR);
      print(barcodeScanRes);
    } on PlatformException {
      barcodeScanRes = 'Failed to get platform version.';
    }
    if (!mounted) return;
    _scanBarcode = barcodeScanRes;
    SharedPreferences prefs = await SharedPreferences.getInstance();
    //Return String
    String value = prefs.getString('token') ?? "";
    print(_scanBarcode);
    final response = await http.get(
        Uri.parse(
            "http://103.195.186.197/nsretailapi/api/stockitems?itemcode=" +
                _scanBarcode),
        headers: {"Authorization": "Bearer $value"});
    _itemsList = loadItems(response.body);
    print('Items : ${_itemsList.length}');
    if (_itemsList.length > 1) {
      print(_scanBarcode + 'morethan 1');
      itemCodeController.text = _scanBarcode;
      String itemName = _itemsList[0].itemName;
      print(itemName);
      itemPriceId = _itemsList[0].itemPriceId;
      String salesPrice = _itemsList[0].salesPrice.toString();
      itemNameController.text = itemName;
      setState(() {
        MRP();
        isDropddownVisible = true;
        isTextVisible = false;
      });
      _fieldFocusChange(context, _itemCodefocus, _MRPfocus);
      //salesPriceController.text = salesPrice;
      _errorMessage = "";
    }
    if (_itemsList.length == 1) {
      print('1 record');
      itemCodeController.text = _scanBarcode;
      String itemName = _itemsList[0].itemName;
      print(itemName);
      itemPriceId = _itemsList[0].itemPriceId;

      String salesPrice = _itemsList[0].salesPrice.toString();
      itemNameController.text = itemName;
      MRPController.text = _itemsList[0].MRP.toString();
      setState(() {
        isDropddownVisible = false;
        isTextVisible = true;
      });

      salesPriceController.text = salesPrice;
      _errorMessage = "";
      _fieldFocusChange(context, _itemCodefocus, _Quantityfocus);
    } else {
      _errorMessage = "Item details not found";
      //clearFields();
    }
  }

  // Platform messages are asynchronous, so we initialize in an async method.
  Future<void> scanBarcodeNormal() async {
    String barcodeScanRes;
    // Platform messages may fail, so we use a try/catch PlatformException.
    try {
      barcodeScanRes = await FlutterBarcodeScanner.scanBarcode(
          '#ff6666', 'Cancel', true, ScanMode.BARCODE);
      print(barcodeScanRes);
    } on PlatformException {
      barcodeScanRes = 'Failed to get platform version.';
    }

    // If the widget was removed from the tree while the asynchronous platform
    // message was in flight, we want to discard the reply rather than calling
    // setState to update our non-existent appearance.
    if (!mounted) return;

    setState(() {
      _scanBarcode = barcodeScanRes;
      if (_scanBarcode != "")
        itemCodeController.text = _scanBarcode;
      else
        _errorMessage = "Item Code not found";
    });
  }

  void getData() async {
    try {
      SharedPreferences prefs = await SharedPreferences.getInstance();
      //Return String
      String value = prefs.getString('token') ?? "";
      print(_scanBarcode);
      final response = await http.get(
          Uri.parse(
              "http://103.195.186.197/nsretailapi/api/stockitems?itemcode=" +
                  _scanBarcode),
          headers: {"Authorization": "Bearer $value"});
      setState(() {
        _itemsList = loadItems(response.body);
        print('Items : ${_itemsList.length}');
      });
    } catch (e) {
      print(e.toString());
    }
  }

  static List<Items> loadItems(String jsonString) {
    print('load Items');

    print(jsonString);
    Map<String, dynamic> map = json.decode(jsonString);
    final parsed = map["data"];
    print(parsed);
    if (parsed != null) {
      //final parsed=json.decode(jsonString).cast<Map<String,dynamic>>();
      return parsed.map<Items>((json) => Items.fromJson(json)).toList();
    } else {
      return [];
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      //resizeToAvoidBottomInset: true,
      appBar: AppBar(
        title: Text('Add Stock Item'),
        elevation: 0,
      ),
      body: ListView(
        padding: EdgeInsets.only(top: 0.0),
        children: [
          textsection(),
          // buttonsection(),
          Center(
            child: Text(_errorMessage),
          )
        ],
      ),
    );
  }

  _fieldFocusChange(
      BuildContext context, FocusNode currentFocus, FocusNode nextFocus) {
    currentFocus.unfocus();
    FocusScope.of(context).requestFocus(nextFocus);
  }

  TextFormField txtItemCode(String title, IconData icon) {
    return TextFormField(
      textInputAction: TextInputAction.done,
      controller: itemCodeController,
      style: TextStyle(color: Colors.lightBlue),
      focusNode: _itemCodefocus,
      autofocus: true,
      onFieldSubmitted: (term) {
        //onChange();
        //_fieldFocusChange(context, _itemCodefocus, _itemNamefocus);
      },
      decoration: InputDecoration(
        hintText: title,
        hintStyle: TextStyle(color: Colors.lightBlue),
        icon: Icon(
          icon,
          color: Colors.lightBlue,
        ),
      ),
    );
  }

  TextFormField txtItemName(String title, IconData icon) {
    return TextFormField(
        textInputAction: TextInputAction.next,
        controller: itemNameController,
        style: TextStyle(color: Colors.lightBlue),
        focusNode: _itemNamefocus,
        enabled: false,
        onFieldSubmitted: (term) {
          _fieldFocusChange(context, _itemNamefocus, _MRPfocus);
        },
        decoration: InputDecoration(
          hintText: title,
          hintStyle: TextStyle(color: Colors.lightBlue),
          icon: Icon(
            icon,
            color: Colors.lightBlue,
          ),
        ));
  }

  txtMRP(String title, IconData icon) {
    return TextFormField(
        textInputAction: TextInputAction.next,
        controller: MRPController,
        style: TextStyle(color: Colors.lightBlue),
        focusNode: _MRPfocus,
        enabled: false,
        onFieldSubmitted: (term) {
          _fieldFocusChange(context, _MRPfocus, _Quantityfocus);
        },
        decoration: InputDecoration(
          hintText: title,
          hintStyle: TextStyle(color: Colors.lightBlue),
          icon: Icon(
            icon,
            color: Colors.lightBlue,
          ),
        ));
  }

  Items MRPdropdownvalue = null;
  DropdownButton MRP() {
    print(_itemsList);
    return DropdownButton<Items>(
      hint: Text('MRP'),
      value: MRPdropdownvalue,
      onChanged: (Items newValue) {
        setState(() {
          MRPdropdownvalue = newValue;
          salesPriceController.text = MRPdropdownvalue.salesPrice.toString();
          _fieldFocusChange(context, _MRPfocus, _Quantityfocus);
        });
      },
      items: _itemsList.map((Items item) {
        return new DropdownMenuItem<Items>(
          value: item,
          child: new Text(
            item.MRP.toString(),
            style: new TextStyle(color: Colors.black),
          ),
        );
      }).toList(),
    );
  }

  FutureBuilder MRPBuilder() {
    // FutureBuilder(
    //   future: getData(),
    //   builder: (context, snapshot) {
    //     if (!snapshot.hasData) {
    //       return Center(
    //         child: DropdownButton(
    //           hint: Text("Preço"),
    //         ),
    //       );
    //     } else {
    //       return Dropdown(title: "Preço", priceProducts: snapshot.data);
    //     }
    //   },
    // );
  }
  Items salePricedropdownvalue = null;
  DropdownButton salePricedropDown() {
    print(_itemsList);
    return DropdownButton<Items>(
      hint: Text('Sale Price'),
      value: salePricedropdownvalue,
      onChanged: (Items newValue) {
        setState(() {
          salePricedropdownvalue = newValue;
        });
      },
      items: _itemsList.map((Items item) {
        return new DropdownMenuItem<Items>(
          value: item,
          child: new Text(
            item.MRP.toString(),
            style: new TextStyle(color: Colors.black),
          ),
        );
      }).toList(),
    );
  }

  TextFormField txtSalesPrice(String title, IconData icon) {
    return TextFormField(
        textInputAction: TextInputAction.next,
        controller: salesPriceController,
        style: TextStyle(color: Colors.lightBlue),
        focusNode: _salesPricefocus,
        enabled: false,
        onFieldSubmitted: (term) {
          _fieldFocusChange(context, _salesPricefocus, _Quantityfocus);
        },
        decoration: InputDecoration(
          hintText: title,
          hintStyle: TextStyle(color: Colors.lightBlue),
          icon: Icon(
            icon,
            color: Colors.lightBlue,
          ),
        ));
  }

  TextFormField txtQuantity(String title, IconData icon) {
    return TextFormField(
        textInputAction: TextInputAction.next,
        controller: QuantityController,
        style: TextStyle(color: Colors.lightBlue),
        focusNode: _Quantityfocus,
        onFieldSubmitted: (term) {
          _fieldFocusChange(context, _Quantityfocus, _submitfocus);
        },
        decoration: InputDecoration(
          hintText: title,
          hintStyle: TextStyle(color: Colors.lightBlue),
          icon: Icon(
            icon,
            color: Colors.lightBlue,
          ),
        ));
  }

  Container buttonsection() {
    return Container(
      //width: MediaQuery.of(context).size.width,
      // height: 40.0,
      // margin: EdgeInsets.only(top: 30.0),
      // padding: EdgeInsets.symmetric(horizontal: 20.0),
      child: RaisedButton(
        focusNode: _submitfocus,
        onPressed: () {
          saveStockCount();
        },
        color: Colors.lightBlue,
        shape: RoundedRectangleBorder(
          borderRadius: BorderRadius.circular(5.0),
        ),
        child: Text(
          'Save',
          style: TextStyle(color: Colors.white, fontSize: 16),
        ),
      ),
    );
  }

  saveStockCount() async {
    try {
      SharedPreferences prefs = await SharedPreferences.getInstance();
      String _itempriceid = "";
      if (isTextVisible)
        _itempriceid = itemPriceId.toString();
      else if (isDropddownVisible)
        _itempriceid = MRPdropdownvalue.itemPriceId.toString();
      //Return String
      String value = prefs.getString('token') ?? "";
      String _userId = prefs.getString('userID') ?? "0";
      String insertUrl =
          "http://103.195.186.197/nsretailapi/api/StockCounting/InsertStockCounting/" +
              widget.nStockcountingID.toString() +
              "/0/" +
              widget.branchId.toString() +
              "/" +
              _userId +
              "/" +
              _itempriceid +
              "/" +
              QuantityController.text;
      print('insert url: ' + insertUrl);

      final response = await http.post(Uri.parse(insertUrl),
          headers: {"Authorization": "Bearer $value"});
      print(response.body);
      if (response.statusCode == 200) {
        setState(() {
          _errorMessage = "Records saved successfully";
          clearFields();
          _fieldFocusChange(context, _Quantityfocus, _itemCodefocus);
        });
      }
    } catch (e) {
      print(e.toString());
    }
  }

  clearFields() {
    itemCodeController.text = "";
    itemNameController.text = "";
    MRPdropdownvalue = null;
    MRP();
    _itemsList = [];
    MRPController.text = "";
    salesPriceController.text = "";
    QuantityController.text = "";
    isDropddownVisible = false;
    isTextVisible = false;
  }

  Container textsection() {
    return Container(
      padding: EdgeInsets.symmetric(horizontal: 20.0, vertical: 30.0),
      // margin: EdgeInsets.only(top: 30.0),
      child: Column(
        children: [
          SizedBox(
            height: 30,
          ),
          Row(
            children: [
              ElevatedButton(
                  onPressed: () => scanQR(), child: Text('Start barcode scan')),
              SizedBox(
                width: 30,
              ),
              buttonsection(),
            ],
          ),
          txtItemCode("Item Code", Icons.code),
          SizedBox(height: 30.0),
          txtItemName("Item Name", Icons.drive_file_rename_outline),
          SizedBox(height: 30.0),
          Visibility(
            child: txtMRP("MRP", Icons.price_change_outlined),
            visible: isTextVisible,
          ),
          Visibility(
            child: MRP(),
            visible: isDropddownVisible,
          ),
          SizedBox(height: 30.0),
          txtSalesPrice("Sales Price", Icons.price_check),
          SizedBox(height: 30.0),
          txtQuantity("Quantity", Icons.production_quantity_limits),
        ],
      ),
    );
  }
}
