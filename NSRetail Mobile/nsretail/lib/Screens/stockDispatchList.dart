// ignore_for_file: file_names

import 'dart:convert';
import 'dart:core';

import 'package:flutter/material.dart';
import 'package:nsretail/Screens/addStockDispatch.dart';
import 'package:nsretail/Screens/addStockItem.dart';
import 'package:nsretail/Screens/branchList.dart';
import 'package:nsretail/Screens/stockCountingList.dart';
import 'package:nsretail/api/Category.dart';
import 'package:nsretail/api/StockCounting.dart';
import 'package:nsretail/api/StockDispatch.dart';
import 'package:nsretail/api/items.dart';
import 'package:rflutter_alert/rflutter_alert.dart';
import 'package:shared_preferences/shared_preferences.dart';

import '../login.dart';
import 'package:http/http.dart' as http;

class stockDispatchList extends StatefulWidget {
  int branchId;
  stockDispatchList(this.branchId);

  @override
  _stockDispatchListState createState() => _stockDispatchListState();
}

class _stockDispatchListState extends State<stockDispatchList> {
  String _errorMessage = "";
  List<StockDispatch> _stockDispatchList = List<StockDispatch>();
  List<StockDispatch> _stockDispatchDetailList = List<StockDispatch>();
  List<Category> _categoryList = List<Category>();
  bool loading = true;
  SharedPreferences sharedPreferences;
  String stUserID;
  int nStockDispatchID = 0;
  int categoryId=0;
  final String url =
      'http://103.195.186.197/nsretailapi/api/stockdispatch?stockcountdetailid=';

  void getStockData(int Id) async {
    try {
      SharedPreferences prefs = await SharedPreferences.getInstance();
      //Return String
      String value = prefs.getString('token') ?? "";
      stUserID = prefs.getString('userID') ?? "";

      print('userid');
      print(stUserID);
      final response = await http.get(
          Uri.parse(url +
              Id.toString() +
              "&userid=" +
              stUserID +
              "&branchid=" +
              widget.branchId.toString()),
          headers: {"Authorization": "Bearer $value"});
      _stockDispatchDetailList = loadStockDispatch(response.body);
      if (_stockDispatchDetailList.length == 0) {
        _errorMessage = "No Results Found";
      }
      print('stockDispatchDetailList: ${_stockDispatchDetailList.length}');
      // print(_filteredBranchList[1].branchName);
      setState(() {
        loading = false;
        _stockDispatchDetailList = _stockDispatchDetailList;
      });
    } catch (e) {
      print(e.toString());
    }
  }
  void getData() async {
    try {
      SharedPreferences prefs = await SharedPreferences.getInstance();
      //Return String
      String value = prefs.getString('token') ?? "";
      stUserID = prefs.getString('userID') ?? "";

      print('userid');
      print(stUserID);
      final response = await http.get(
          Uri.parse(url +
              "0&userid=" +
              stUserID +
              "&branchid=" +
              widget.branchId.toString()),
          headers: {"Authorization": "Bearer $value"});
      _stockDispatchList = loadStockDispatch(response.body);
      if (_stockDispatchList.length == 0) {
        _errorMessage = "No Results Found";
      } else {
        print('Stock Dispatch List: ${_stockDispatchList.length}');
        print(_stockDispatchList);
        setState(() {
          nStockDispatchID = _stockDispatchList[0].stockCountId;
          loading = false;
        });
      }
    } catch (e) {
      print(e.toString());
    }
  }
  void getCategoryData() async {
    try {
      SharedPreferences prefs = await SharedPreferences.getInstance();
      //Return String
      String value = prefs.getString('token') ?? "";
      stUserID = prefs.getString('userID') ?? "";

      print('userid');
      print(stUserID);
      final response = await http.get(
          Uri.parse('http://103.195.186.197/nsretailapi/api/categories'),
          headers: {"Authorization": "Bearer $value"});
      _categoryList = loadCategories(response.body);
      if (_categoryList.length == 0) {
        _errorMessage = "No Results Found";
      } else {
        print('_categoryList List: ${_categoryList.length}');
        print(_categoryList);
        setState(() {
          _categoryList =_categoryList;
        });
      }
    } catch (e) {
      print(e.toString());
    }
  }

  void updateStatus() async {
    String value = "";
    String stockCountingId = "";
    try {
      SharedPreferences prefs = await SharedPreferences.getInstance();
      //Return String
      value = prefs.getString('token') ?? "";
      stUserID = prefs.getString('userID') ?? "";

      print('userid');
      print(stUserID);
      //getData();
      print('stock count id:');
      stockCountingId = _stockDispatchList[0].stockCountId.toString();
      print(stockCountingId);
      final response = await http.post(
          Uri.parse(
              "http://103.195.186.197/nsretailapi/api/stockdispatch/UpdateStatus/" +
                  stockCountingId),
          headers: {"Authorization": "Bearer $value"});
      print("http://103.195.186.197/nsretailapi/api/StockCounting/UpdateStatus/" +
          stockCountingId);
      if (response.statusCode == 200) {
        setState(() {
          getData();
        });
      }
    } catch (e) {
      final response = await http.post(
          Uri.parse(
              "http://103.195.186.197/nsretailapi/api/stockdispatch/UpdateStatus/" +
                  stockCountingId),
          headers: {"Authorization": "Bearer $value"});
      print("http://103.195.186.197/nsretailapi/api/stockdispatch/UpdateStatus/" +
          stockCountingId);
      if (response.statusCode == 200) {
        setState(() {
          getData();
        });
      }
      print(e.toString());
    }
  }

  static List<StockDispatch> loadStockDispatch(String jsonString) {
    print('loadStockDispatch');

    print(jsonString);
    Map<String, dynamic> map = json.decode(jsonString);
    final parsed = map["data"];
    print(parsed);
    //final parsed=json.decode(jsonString).cast<Map<String,dynamic>>();
    return parsed
        .map<StockDispatch>((json) => StockDispatch.fromJson(json))
        .toList();
  }
  static List<Category> loadCategories(String jsonString) {
    print('load categories');

    print(jsonString);
    Map<String, dynamic> map = json.decode(jsonString);
    final parsed = map["data"];
    print(parsed);
    //final parsed=json.decode(jsonString).cast<Map<String,dynamic>>();
    return parsed
        .map<Category>((json) => Category.fromJson(json))
        .toList();
  }
  @override
  void initState() {
    // TODO: implement initState
    super.initState();
    checkLoginState();
    this.getData();
    this.getCategoryData();
  }

  checkLoginState() async {
    sharedPreferences = await SharedPreferences.getInstance();
    if (sharedPreferences.getString('token') == null) {
      Navigator.of(context).pushAndRemoveUntil(
          MaterialPageRoute(builder: (BuildContext context) => Login()),
          (route) => false);
    }
  }

  _openPopup(context, int stockDetailId, int stockId, int itemPriceId) {
    TextEditingController itemNameController = new TextEditingController();
    TextEditingController QuantityController = new TextEditingController();
    getStockData(stockDetailId);
    print('Detail Id' + stockDetailId.toString());
    print(_stockDispatchDetailList);
    itemNameController.text = _stockDispatchDetailList[0].itemName.toString();
    QuantityController.text = _stockDispatchDetailList[0].quantity.toString();
    Alert(
        context: context,
        title: "Edit",
        content: Column(
          children: <Widget>[
            TextField(
              controller: itemNameController,
              decoration: InputDecoration(
                  icon: Icon(Icons.account_circle), labelText: "Item Name" //,
                  ),
            ),
            TextField(
              controller: QuantityController,
              decoration: InputDecoration(
                  icon: Icon(Icons.lock), labelText: "Quantity" // ,
                  ),
            ),
          ],
        ),
        buttons: [
          DialogButton(
            onPressed: () async {
              try {
                SharedPreferences prefs = await SharedPreferences.getInstance();
                //Return String
                String value = prefs.getString('token') ?? "";
                String _userId = prefs.getString('userID') ?? "0";
                print(stockDetailId);
                final response = await http.post(
                    Uri.parse(
                        "http://103.195.186.197/nsretailapi/api/StockCounting/InsertStockCounting/" +
                            stockId.toString() +
                            "/" +
                            stockDetailId.toString() +
                            "/" +
                            widget.branchId.toString() +
                            "/" +
                            _userId +
                            "/" +
                            itemPriceId.toString() +
                            "/" +
                            QuantityController.text),
                    headers: {"Authorization": "Bearer $value"});
                if (response.statusCode != 200) {
                  _errorMessage = "Record not Modified. Some error occurred.";
                } else {
                  setState(() {
                    getData();
                  });
                }
              } catch (e) {
                print(e.toString());
              }
              Navigator.pop(context);
            },
            child: Text(
              "Submit",
              style: TextStyle(color: Colors.white, fontSize: 20),
            ),
          )
        ]).show();
  }

  Category Categorydropdownvalue = null;
  DropdownButton Categories() {
    print(_categoryList);
    return DropdownButton<Category>(
      hint: Text('Category'),
      value: Categorydropdownvalue,
      onChanged: (Category newValue) {
        setState(() {
          Categorydropdownvalue = newValue;
          categoryId=Categorydropdownvalue.Id;
        });
      },
      items: _categoryList.map((Category item) {
        return new DropdownMenuItem<Category>(
          value: item,
          child: new Text(
            item.categoryName.toString(),
            style: new TextStyle(color: Colors.black),
          ),
        );
      }).toList(),
    );
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Stock Dispatch'),
        actions: [

          RaisedButton(
            onPressed: () {
              updateStatus();
            },
            color: Colors.lightBlue,
            shape: RoundedRectangleBorder(
              borderRadius: BorderRadius.circular(5.0),
            ),
            child: Text(
              'Submit',
              style: TextStyle(color: Colors.white, fontSize: 16),
            ),
          ),
        ],
      ),
      drawer: Drawer(
        // Add a ListView to the drawer. This ensures the user can scroll
        // through the options in the drawer if there isn't enough vertical
        // space to fit everything.
        child: ListView(
          // Important: Remove any padding from the ListView.
          padding: EdgeInsets.zero,
          children: [
            const DrawerHeader(
              decoration: BoxDecoration(
                color: Colors.blue,
              ),
              child: Image(
                image: AssetImage('images/logo.png'),
              ),
            ),
            ListTile(
              title: const Text('Branches'),
              onTap: () {
                Navigator.of(context).pushAndRemoveUntil(
                    MaterialPageRoute(
                        builder: (BuildContext context) => BranchList('')),
                    (route) => false);
                //Navigator.pop(context);
              },
            ),
            ListTile(
              title: const Text('Stock Counting'),
              onTap: () {
                Navigator.of(context).pushAndRemoveUntil(
                    MaterialPageRoute(
                        builder: (BuildContext context) =>
                            stockCountingList(widget.branchId,'','')),
                    (route) => false);
                //Navigator.pop(context);
              },
            ),
            ListTile(
              title: const Text('Stock Dispatch'),
              onTap: () {
                Navigator.of(context).pushAndRemoveUntil(
                    MaterialPageRoute(
                        builder: (BuildContext context) =>
                            stockDispatchList(widget.branchId)),
                    (route) => false);
                //Navigator.pop(context);
              },
            ),
            ListTile(
              title: const Text('Logout'),
              onTap: () {
                sharedPreferences.clear();
                sharedPreferences.setString("token", "");
                sharedPreferences.remove("token");
                checkLoginState();
                print('logout pressed');
              },
            ),
          ],
        ),
      ),
      body: Column(
        children: [
          Categories(),
          Card(
            child: _stockDispatchList.isEmpty
                ? Center(child: Text('No Results Found'))
                : ListView.separated(
                    // Let the ListView know how many items it needs to build.
                    itemCount: _stockDispatchList.length,

                    // Provide a builder function. This is where the magic happens.
                    // Convert each item into a widget based on the type of item it is.
                    itemBuilder: (context, index) {
                      final item = _stockDispatchList[index];

                      return ListTile(
                        onLongPress: () {},
                        onTap: () {
                          _openPopup(context, item.stockCountDetailid,
                              item.stockCountId, item.itemPriceId);
                        },
                        title: Text("Item Name: " + item.itemName.toString()),
                        subtitle: Text("MRP: " +
                            item.MRP.toString() +
                            ", SalesPrice: " +
                            item.salesPrice.toString() +
                            ", Quantity: " +
                            item.quantity.toString()),
                        trailing: IconButton(
                          icon: Icon(Icons.delete),
                          onPressed: () async {
                            try {
                              SharedPreferences prefs =
                                  await SharedPreferences.getInstance();
                              //Return String
                              String value = prefs.getString('token') ?? "";
                              String _userId = prefs.getString('userID') ?? "0";
                              print(item.stockCountDetailid);
                              final response = await http.post(
                                  Uri.parse(
                                      "http://103.195.186.197/nsretailapi/api/stockdispatch/DeleteStockCounting/" +
                                          item.stockCountDetailid.toString()),
                                  headers: {"Authorization": "Bearer $value"});
                              print(response.body);
                              if (response.statusCode == 200) {
                                setState(() {
                                  getData();
                                });
                              } else {
                                _errorMessage =
                                    "Record not deleted. Some error occurred.";
                              }
                            } catch (e) {
                              print(e.toString());
                            }
                          },
                        ),
                      );
                    },

                    separatorBuilder: (context, index) {
                      return Divider();
                    },
                  ),
          ),
        ],
      ),
      floatingActionButton: FloatingActionButton(
        child: IconButton(
          icon: Icon(Icons.add),
          onPressed: () {
            Navigator.push(
              context,
              MaterialPageRoute(
                builder: (context) =>
                    StockDispatchItem(widget.branchId, nStockDispatchID,categoryId),
              ),
            ).then((value) => setState(() {
                  this.getData();
                }));
          },
        ),
      ),
    );
  }
}
