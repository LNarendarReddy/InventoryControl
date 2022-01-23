class Branches{
  int branchId;
  String branchName;
  String branchCode;
  Branches({this.branchId,this.branchName,this.branchCode});
  factory Branches.fromJson(Map<String,dynamic> parsed){
    return Branches(
      branchId: parsed["branchid"],
      branchName: parsed["branchname"] as String,
      branchCode: parsed["branchcode"] as String
    );
  }
}