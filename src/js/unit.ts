/*
* 2018..8.6
* sky创建的unit公共函数文件
* */
const unit = {
  // 输入数组，返回一个布尔值
  GetListBool: function (arr, str) {
    let bool = true;
    arr.map((item, index) => {
      if (item.title === str) {
        bool = false;
        return;
      }
    })
    return bool;
  },

}
export default unit;
