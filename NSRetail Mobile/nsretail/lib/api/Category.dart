class Category {
  int Id;
  String categoryName;
  Category({this.Id, this.categoryName});
  factory Category.fromJson(Map<String, dynamic> parsed) {
    return Category(
      Id: parsed["categoryid"],
      categoryName: parsed["categoryname"] as String,
    );
  }
}
