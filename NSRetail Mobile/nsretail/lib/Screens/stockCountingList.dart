// ignore_for_file: file_names

import 'dart:convert';

import 'package:autocomplete_textfield/autocomplete_textfield.dart';
import 'package:flutter/material.dart';
import 'package:nsretail/Screens/addStockItem.dart';
import 'package:nsretail/Screens/branchList.dart';
import 'package:nsretail/Screens/stockDispatchList.dart';
import 'package:nsretail/api/StockCounting.dart';
import 'package:nsretail/api/items.dart';
import 'package:rflutter_alert/rflutter_alert.dart';
import 'package:shared_preferences/shared_preferences.dart';

import '../login.dart';
import 'package:http/http.dart' as http;

class stockCountingList extends StatefulWidget {
  int branchId;
  String branchName;
  String userName;
  stockCountingList(this.branchId, this.branchName, this.userName);

  @override
  _stockCountingListState createState() => _stockCountingListState();
}

class _stockCountingListState extends State<stockCountingList> {
  AutoCompleteTextField searchTextField;
  GlobalKey<AutoCompleteTextFieldState<StockCounting>> key = new GlobalKey();
  String _errorMessage = "";
  List<StockCounting> _itemsList = List<StockCounting>();
  List<StockCounting> _filtereditemsList = List<StockCounting>();
  List<StockCounting> _stockItemsList = List<StockCounting>();
  bool loading = true;
  SharedPreferences sharedPreferences;
  String stUserID;
  int nStockcountingID = 0;
  final String url =
      'http://103.195.186.197/nsretailapi/api/stockcounting?stockcountdetailid=';

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
      _stockItemsList = loadStockCounting(response.body);
      if (_stockItemsList.length == 0) {
        _errorMessage = "No Results Found";
      }
      print('Branches: ${_stockItemsList.length}');
      // print(_filteredBranchList[1].branchName);
      setState(() {
        loading = false;
        _stockItemsList = _stockItemsList;
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
      _itemsList = loadStockCounting(response.body);
      _filtereditemsList = _itemsList;
      if (_itemsList.length == 0) {
        _errorMessage = "No Results Found";
      } else {
        print('Stock Count List: ${_itemsList.length}');
        print(_itemsList);
        setState(() {
          nStockcountingID = _itemsList[0].stockCountId;
          loading = false;
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
      stockCountingId = _itemsList[0].stockCountId.toString();
      print(stockCountingId);
      final response = await http.post(
          Uri.parse(
              "http://103.195.186.197/nsretailapi/api/StockCounting/UpdateStatus/" +
                  stockCountingId +
                  "/" +
                  stUserID),
          headers: {"Authorization": "Bearer $value"});
      print(
          "http://103.195.186.197/nsretailapi/api/StockCounting/UpdateStatus/" +
              stockCountingId +
              "/" +
              stUserID);
      if (response.statusCode == 200) {
        setState(() {
          getData();
          _itemsList.clear();
          Navigator.push(
            context,
            MaterialPageRoute(
              builder: (context) =>
                  BranchList(widget.userName),
            ),
          );

        });
      }
    } catch (e) {
      final response = await http.post(
          Uri.parse(
              "http://103.195.186.197/nsretailapi/api/StockCounting/UpdateStatus/" +
                  stockCountingId +
                  "/" +
                  stUserID),
          headers: {"Authorization": "Bearer $value"});
      print(
          "http://103.195.186.197/nsretailapi/api/StockCounting/UpdateStatus/" +
              stockCountingId +
              "/" +
              stUserID);
      if (response.statusCode == 200) {
        //setState(() {
        getData();
        //stockCountingList(widget.branchId);
        //});
      }
      print(e.toString());
    }
  }

  static List<StockCounting> loadStockCounting(String jsonString) {
    print('loadbranches');

    print(jsonString);
    Map<String, dynamic> map = json.decode(jsonString);
    final parsed = map["data"];
    print(parsed);
    //final parsed=json.decode(jsonString).cast<Map<String,dynamic>>();
    if(parsed!=null) {
      return parsed
          .map<StockCounting>((json) => StockCounting.fromJson(json))
          .toList();
    }
    else {
      return [];
    }
  }

  @override
  void initState() {
    // TODO: implement initState
    super.initState();
    checkLoginState();
    this.getData();
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
    print(_stockItemsList);
    itemNameController.text = _stockItemsList[0].itemName.toString();
    QuantityController.text = _stockItemsList[0].quantity.toString();
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

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Stock Couting'),
        actions: [
          new Container(),
          new Center(
            child: Text(widget.userName,),
          )
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
            // ListTile(
            //   title: const Text('Branches'),
            //   onTap: () {
            //     Navigator.of(context).pushAndRemoveUntil(
            //         MaterialPageRoute(
            //             builder: (BuildContext context) => BranchList()),
            //         (route) => false);
            //     //Navigator.pop(context);
            //   },
            // ),
            // ListTile(
            //   title: const Text('Stock Counting'),
            //   onTap: () {
            //     Navigator.of(context).pushAndRemoveUntil(
            //         MaterialPageRoute(
            //             builder: (BuildContext context) =>
            //                 stockCountingList(widget.branchId)),
            //         (route) => false);
            //     //Navigator.pop(context);
            //   },
            // ),

            // ListTile(
            //   title: const Text('Stock Dispatch'),
            //   onTap: () {
            //     Navigator.of(context).pushAndRemoveUntil(
            //         MaterialPageRoute(
            //             builder: (BuildContext context) =>
            //                 stockDispatchList(widget.branchId)),
            //         (route) => false);
            //     //Navigator.pop(context);
            //   },
            // ),
            ListTile(
              title: const Text('Logout   Ver 1.1.0'),
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
      body: Center(
        child: Column(
          children: [
            AppBar(
              elevation: 0.0,
              automaticallyImplyLeading: false,
              title:
                  widget.branchName == '' ? Text('') : Text(widget.branchName),
              actions: [
                RaisedButton(
                  color: Colors.blue,
                  child: Text('Submit',style: TextStyle(color: Colors.white),),
                  onPressed: () {
                    updateStatus();
                  },
                )
              ],
            ),
            TextField(
              decoration: InputDecoration(
                contentPadding: EdgeInsets.all(10.0),
                hintText: 'Enter Item Name or Item Code',
              ),
              onSubmitted: (string) {},
              onChanged: (string) {
                setState(() {
                  _filtereditemsList = _itemsList
                      .where((element) => (element.itemCode
                          .toLowerCase()
                          .contains(string.toLowerCase())) || (element.itemName
                      .toLowerCase()
                      .contains(string.toLowerCase())))
                      .toList();
                });
              },
            ),
            Expanded(
              child: ListView.builder(
                  padding: EdgeInsets.all(10.0),
                  itemCount: _filtereditemsList.length != null
                      ? _filtereditemsList.length
                      : 0,
                  itemBuilder: (BuildContext context, int index) {
                    return ListTile(
                      title: Text(_filtereditemsList[index].itemCode.toString() + "-"+_filtereditemsList[index].itemName.toString()),
                      subtitle: Text("MRP: " +
                          _filtereditemsList[index].MRP.toString() +
                          ", SalesPrice: " +
                          _filtereditemsList[index].salesPrice.toString() +
                          ", Quantity: " +
                          _filtereditemsList[index].quantity.toString()),
                      trailing: IconButton(
                        icon: Icon(Icons.delete),
                        onPressed: () async {
                          try {
                            SharedPreferences prefs =
                            await SharedPreferences.getInstance();
                            //Return String
                            String value = prefs.getString('token') ?? "";
                            String _userId = prefs.getString('userID') ?? "0";
                            print(_filtereditemsList[index].stockCountDetailid);
                            final response = await http.post(
                                Uri.parse(
                                    "http://103.195.186.197/nsretailapi/api/StockCounting/DeleteStockCounting/" +
                                        _filtereditemsList[index].stockCountDetailid.toString()),
                                headers: {"Authorization": "Bearer $value"});
                            print(response.body);
                            if (response.statusCode == 200) {
                              //Navigator.pop(context);
                              print('record deleted');
                              setState(() {
                                getData();
                                _itemsList.removeAt(index);
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
                      onTap: () {
                        _openPopup(context, _filtereditemsList[index].stockCountDetailid,
                            _filtereditemsList[index].stockCountId, _filtereditemsList[index].itemPriceId);
                      },
                    );
                  }),

              // Card(
              //   child: _itemsList.isEmpty
              //       ? Center(child: Text('No Results Found'))
              //       : ListView.separated(
              //     // Let the ListView know how many items it needs to build.
              //     itemCount: _filtereditemsList.length != null
              //         ? _filtereditemsList.length
              //         : 0,
              //
              //     // Provide a builder function. This is where the magic happens.
              //     // Convert each item into a widget based on the type of item it is.
              //     itemBuilder: (context, index) {
              //       final item = _itemsList[index];
              //
              //       return ListTile(
              //         onLongPress: () {},
              //         onTap: () {
              //           _openPopup(context, item.stockCountDetailid,
              //               item.stockCountId, item.itemPriceId);
              //         },
              //         title: Text("Item Code: " + item.itemCode.toString()),
              //         subtitle: Text("MRP: " +
              //             item.MRP.toString() +
              //             ", SalesPrice: " +
              //             item.salesPrice.toString() +
              //             ", Quantity: " +
              //             item.quantity.toString()),
              //         trailing: IconButton(
              //           icon: Icon(Icons.delete),
              //           onPressed: () async {
              //             try {
              //               SharedPreferences prefs =
              //               await SharedPreferences.getInstance();
              //               //Return String
              //               String value = prefs.getString('token') ?? "";
              //               String _userId = prefs.getString('userID') ?? "0";
              //               print(item.stockCountDetailid);
              //               final response = await http.post(
              //                   Uri.parse(
              //                       "http://103.195.186.197/nsretailapi/api/StockCounting/DeleteStockCounting/" +
              //                           item.stockCountDetailid.toString()),
              //                   headers: {"Authorization": "Bearer $value"});
              //               print(response.body);
              //               if (response.statusCode == 200) {
              //                 //Navigator.pop(context);
              //                 print('record deleted');
              //                 setState(() {
              //                   getData();
              //                   _itemsList.removeAt(index);
              //                 });
              //               } else {
              //                 _errorMessage =
              //                 "Record not deleted. Some error occurred.";
              //               }
              //             } catch (e) {
              //               print(e.toString());
              //             }
              //           },
              //         ),
              //       );
              //     },
              //
              //     separatorBuilder: (context, index) {
              //       return Divider();
              //     },
              //   ),
              // ),
            )
          ],
        ),
      ),
      floatingActionButtonLocation: FloatingActionButtonLocation.centerFloat,
      floatingActionButton: Container(
        padding: EdgeInsets.symmetric(vertical: 0, horizontal: 20.0),
        child: Row(
          mainAxisAlignment: MainAxisAlignment.spaceBetween,
          children: <Widget>[
            // FloatingActionButton(
            //   child: IconButton(
            //     onPressed: () {
            //       updateStatus();
            //     },
            //     color: Colors.lightBlue,
            //     icon: Icon(
            //       Icons.subdirectory_arrow_right,
            //       color: Colors.white,
            //     ),
            //   ),
            // ),
            FloatingActionButton(
              child: IconButton(
                icon: Icon(Icons.add),
                onPressed: () {
                  Navigator.push(
                    context,
                    MaterialPageRoute(
                      builder: (context) =>
                          StockItem(widget.branchId, nStockcountingID),
                    ),
                  ).then((value) => setState(() {
                        this.getData();
                      }));
                },
              ),
            ),
          ],
        ),
      ),
    );
  }
}
