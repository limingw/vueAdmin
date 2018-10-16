
$.extend($.fn.dataTable.defaults, {
    "searching": false,//datatables自带的搜索
    "ordering": true,//是否开启排序
    "bLengthChange": false,//是否允许终端用户从一个选择列表中选择分页的页数
    "bProcessing": true,//加载中
    "bPaginate": true,  //是否开启分页功能
    "serverSide": true,//开启服务器分页模式
    "iDisplayLength": 10,//分页大小
    "oLanguage": {
        "sProcessing": "处理中...",
        "sLengthMenu": "显示 _MENU_ 项结果",
        "sZeroRecords": "没有匹配结果",
        "sInfo": "显示第 _START_ 至 _END_ 项结果，共 _TOTAL_ 项",
        "sInfoEmpty": "显示第 0 至 0 项结果，共 0 项",
        "sInfoFiltered": "(由 _MAX_ 项结果过滤)",
        "sInfoPostFix": "",
        "sSearch": "搜索：",
        "sUrl": "",
        "sEmptyTable": "表中数据为空",
        "sLoadingRecords": "载入中...",
        "sInfoThousands": ",",
        "oPaginate": {
            "sFirst": "首页",
            "sPrevious": "上页",
            "sNext": "下页",
            "sLast": "末页"
        },
        "oAria": {
            "sSortAscending": ": 以升序排列此列",
            "sSortDescending": ": 以降序排列此列"
        }
    }
});