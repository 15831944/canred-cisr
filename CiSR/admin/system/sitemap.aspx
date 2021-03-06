﻿
<%@ Page Title="" Language="C#" MasterPageFile="~/mpStand.Master" AutoEventWireup="true" CodeBehind="sitemap.aspx.cs" Inherits="Web.admin.system.sitemap" EnableViewState="false"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript" src='<%= Page.ResolveUrl("~/js/shared/include-ext.js")%>'></script>
<script type="text/javascript" src='<%= Page.ResolveUrl("~/Proxy.ashx")%>'></script>
<script type="text/javascript" src='<%= Page.ResolveUrl("~/pageJs/AllModel.js")+ "?cache="+IST.Util.UID.Instance.GetUniqueID()%>'></script>
<script type="text/javascript" src='<%= Page.ResolveUrl("~/pageJs/Ext.CompanyForm.js")%>'></script>
<script type="text/javascript" src='<%= Page.ResolveUrl("~/pageJs/Ext.AppPageForm.js")%>'></script>
<script type="text/javascript" src='<%= Page.ResolveUrl("~/pageJs/Ext.AppPagePicker.js")%>'></script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<script language="javascript" type="text/javascript">
var thisTreeStore = undefined;
var AppPageQuery = undefined;
var myForm = undefined;
// Ext.require([
//     'Ext.grid.*',
//     'Ext.data.*',
//     'Ext.util.*',
//     'Ext.toolbar.Paging',
//     'Ext.ux.PreviewPlugin',
//     'Ext.ModelManager',
//     'Ext.tip.QuickTipManager'
// ]);

var SiteMapVTaskFlag = false;
var SiteMapVTask = {
    run: function () {
        if (SiteMapVTaskFlag == true) {
            Ext.getCmp('SiteMap_Tree').expandAll();
            SiteMapVTaskFlag = false;
        }
    },
    interval: 1000
}
Ext.TaskManager.start(SiteMapVTask);


var PARENTUUID = undefined;

function openOrgn(uuid, parendUuid) {
    PARENTUUID = parendUuid;
    if (myForm == undefined) {
        myForm = Ext.create('AppPagePicker', {});
        myForm.on('closeEvent', function (obj) {
        });
        myForm.on('selectedEvent', function (obj, jsonObj) {
            WS.SiteMapAction.addSiteMap(PARENTUUID, jsonObj.UUID);
            obj.hide();
            var btnQuery = Ext.getCmp('function_Query_Button')
            btnQuery.handler.call(btnQuery.scope)
            SiteMapVTaskFlag = true;
        });
    }
    myForm.setTitle('<img src="' + SYSTEM_URL_ROOT + '/css/images/map.png" style="height:20px;vertical-align:middle;margin-right:5px;">挑選網站地圖功能');
    myForm.applicationHeadUuid = Ext.getCmp('function_Query_Application').getValue();
    if (uuid == "undefined")
        myForm.uuid = undefined;
    else
        myForm.uuid = uuid;

    myForm.show();
}
Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".AppPageAction"));
Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".SiteMapAction"));
Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".ApplicationAction"));

function delSitmap(uuid) {
    Ext.Msg.show({
        title: '刪除節點操作',
        msg: '確定執行刪除動作?',
        buttons: Ext.Msg.YESNO,
        fn: function (btn) {
            if (btn == "yes") {
                var appliction_uuid = Ext.getCmp('function_Query_Application').getValue();
                WS.SiteMapAction.deleteSiteMap(uuid, appliction_uuid, function (json) {
                    if (json.success == false) {
                        Ext.Msg.show({
                            title: '系統通知',
                            msg: json.message,
                            buttons: Ext.Msg.YES
                        });
                    } else {
                        var btnQuery = Ext.getCmp('function_Query_Button')
                        btnQuery.handler.call(btnQuery.scope)
                        SiteMapVTaskFlag = true;
                    }
                });
            }
        }
    });

}


Ext.onReady(function () {
    /*:::加入Direct:::*/

    Ext.QuickTips.init();
    /*:::設定Compnay Store物件:::*/
    var storeApplication =
        Ext.create('Ext.data.Store', {
            successProperty: 'success',
            autoLoad: true,
            /*:::Table設定:::*/
            model: 'APPLICATION',
            pageSize: 10,
            proxy: {
                type: 'direct',
                api: {
                    read: WS.ApplicationAction.loadApplication
                },
                reader: {
                    root: 'data'
                },
                paramsAsHash: true,
                /*:::Direct Method Parameter Setting:::*/
                paramOrder: ['pKeyword', 'page', 'limit', 'sort', 'dir'],
                extraParams: {
                    pKeyword: ''
                },
                simpleSortMode: true,
                listeners: {
                    exception: function (proxy, response, operation) {
                        Ext.MessageBox.show({
                            title: 'REMOTE EXCEPTION',
                            msg: operation.getError(),
                            icon: Ext.MessageBox.ERROR,
                            buttons: Ext.Msg.OK
                        });
                    },
                    beforeload: function () {
                        alert('beforeload proxy');
                    }
                }
            },
            listeners: {
                write: function (proxy, operation) {},
                read: function (proxy, operation) {},
                beforeload: function () {},
                afterload: function () {},
                load: function () {
                    if (storeApplication.getCount() > 0) {
                        Ext.getCmp('function_Query_Application').setValue(storeApplication.data.getAt(0).data['UUID']); 
                    }
                }
            },
            remoteSort: true,
            sorters: [{
                property: 'NAME'
            }]
        });

    
    Ext.define('SitemapVTree', {
        extend: 'Ext.data.TreeStore',
        root: {
            expanded: true
        },
        autoLoad: false,
        successProperty: 'success',
        model: 'Model.SiteMap',
        nodeParam: 'id',
        proxy: {
            paramOrder: ['UUID'],
            type: 'direct',
            directFn: WS.SiteMapAction.loadSiteMapTree,
            extraParams: {
                "UUID": ''
            }
        }
    });



    thisTreeStore = Ext.create('SitemapVTree', {});

    function isActiveRenderer(value, id, r) {
        if (value == "Y")
            return "<img src='../../css/custimages/active.gif' style='height:20px;vertical-align:middle'>";
        else if (value == "N")
            return "<img src='../../css/custimages/unactive.gif' style='height:20px;vertical-align:middle'>";
    }

    /*設定元件*/
    AppPageQuery = Ext.widget({
        xtype: 'panel',
        /*網站地圖*/
        title: '<img src="' + SYSTEM_URL_ROOT + '/css/images/map.png" style="height:20px;vertical-align:middle;margin-right:5px;">網站地圖',
        frame: true,
        //padding: 5,
        //autoHeight: true,
        height:$(this).height()-140,
        autoWidth: true,
        items: [{
            layout: 'column',
            padding: 10,
            border: false,
            items: [{
                    style: 'display:block; padding:4px 0px 0px 0px',
                    xtype: 'label',
                    text: '系統：'
                }, {
                    xtype: 'combo',
                    editable: false,
                    store: storeApplication,
                    displayField: 'NAME',
                    valueField: 'UUID',
                    enableKeyEvents: true,
                    id: 'function_Query_Application',
                    listeners: {
                        'change': function (obj, value) {
                            var btnQuery = Ext.getCmp('function_Query_Button');
                            btnQuery.handler.call(btnQuery.scope);
                            SiteMapVTaskFlag = true;
                        },
                        keyup: function (e, t, eOpts) {
                            if (t.button == 12) {
                                this.up('panel').down("#btnQuery").handler();
                            }
                        }
                    }
                }, {
                    xtype: 'label',
                    text: '',
                    style: 'display:block; padding:4px 4px 4px 4px'
                }, {
                    xtype: 'label',
                    text: '',
                    style: 'display:block; padding:4px 4px 4px 4px'
                }, {
                    xtype: 'button',
                    text: '<img src="' + SYSTEM_URL_ROOT + '/css/custimages/search.gif" style="height:20px;vertical-align:middle;margin-top:-2px;margin-right:5px;">查詢',
                    //style: 'display:block; padding:4px 0px 0px 0px',
                    id: 'function_Query_Button',
                    itemId: 'btnQuery',
                    handler: function () {
                        WS.SiteMapAction.loadTreeRoot(Ext.getCmp('function_Query_Application').getValue(), function (data) {
                            thisTreeStore.load({
                                params: {
                                    UUID: data.UUID
                                }
                            });
                            SiteMapVTaskFlag = true;
                        });

                    }
                }, {
                    xtype: 'label',
                    text: '',
                    style: 'display:block; padding:4px 4px 4px 4px'
                }

            ]
        }, {
            xtype: 'button',
            text: '<img src="' + SYSTEM_URL_ROOT + '/css/custimages/add.gif" style="height:12px;vertical-align:middle;margin-top:-2px;margin-right:5px;">新增網站子節點',
            style: 'margin-left:5px',
            handler: function () {
                WS.SiteMapAction.loadTreeRoot(Ext.getCmp('function_Query_Application').getValue(), function (data) {
                    PARENTUUID = data.UUID;
                    if (myForm == undefined) {
                        myForm = Ext.create('AppPagePicker', {});
                        myForm.on('closeEvent', function (obj) {});
                        myForm.on('selectedEvent', function (obj, jsonObj) {
                               
                                WS.SiteMapAction.addSiteMap(PARENTUUID, jsonObj.UUID,function(obj,jsonObj2){
                                        if(jsonObj2.result.success==false){
                                          Ext.MessageBox.show({
                                            title: 'Warning',
                                            msg: jsonObj2.result.message,
                                            icon: Ext.MessageBox.ERROR,
                                            buttons: Ext.Msg.OK
                                        });                                      
                                        }
                                          
                                });
                                obj.hide();
                                var btnQuery = Ext.getCmp('function_Query_Button')
                                btnQuery.handler.call(btnQuery.scope)
                                SiteMapVTaskFlag = true;
                            
                        });
                    }
                    myForm.setTitle('<img src="' + SYSTEM_URL_ROOT + '/css/images/map.png" style="height:20px;vertical-align:middle;margin-right:5px;">挑選網站地圖功能');
                    myForm.applicationHeadUuid = Ext.getCmp('function_Query_Application').getValue();
                    myForm.show();
                });

                return;
                if (myForm == undefined) {
                    myForm = Ext.create('OperationalBoundaryForm', {});
                    myForm.on('closeEvent', function (obj) {
                        location.href = location.href;
                    });
                }
                myForm.setTitle('組織邊界【新增】');
                myForm.uuid = undefined;
                myForm.parentUuid = rootUuid;
                myForm.is_operational_boundary = "N";
                myForm.show();
            }
        }, {
            xtype: 'treepanel',
            fieldLabel: '網站地圖',
            id: 'SiteMap_Tree',
            padding: 5,
            border:true,
            //height:$(this).height()-240,
            //minHeight: 400,
            store: thisTreeStore,
            multiSelect: true,
            rootVisible: false,
            useArrows: true,
            columns: [{
                xtype: 'treecolumn',
                text: '地圖功能',
                flex: 2,
                sortable: true,
                dataIndex: 'NAME'
            }, {
                text: '描述',
                flex: 1,
                dataIndex: 'DESCRIPTION',
                align: 'left',
                sortable: true
            }, {
                text: "維護",
                dataIndex: 'UUID',
                align: 'center',
                flex: 1.5,
                renderer: function (value, m, r) {
                    var id = Ext.id();
                    var dom;
                    if (r.getData().leaf == true) {
                        dom = "<input type='button' class='del-button' onclick='delSitmap(\"" + value + "\");' value='刪除'/>  <input type='button'  class='add-button'   onclick='openOrgn(\"undefined\",\"" + value + "\");' value='新增子節點'/>";                        
                    } else {
                    dom = "<input type='button' class='del-button' value='刪除'/>  <input type='button'  class='add-button'   onclick='openOrgn(\"undefined\",\"" + value + "\");' value='新增子節點'/>";
                    }
                    return dom;
                },
                sortable: false,
                hideable: false
            }],
            listeners: {
                beforeload: function (tree, node,eOpts) {
                    // if (node.isComplete() == false) {
                    //     thisTreeStore.getProxy().setExtraParam('UUID', node.node.data['UUID']);
                    // }
                    if (node.isComplete() == false) {
                        //thisTreeStore.getProxy().setExtraParam('UUID', node.node.raw['UUID']);
                        //alert(node.getParams()["UUID"]);
                        if(node.getParams()["UUID"]!=undefined){
                            thisTreeStore.getProxy().setExtraParam('UUID', node.getParams()["UUID"]);
                        }else{
                            thisTreeStore.getProxy().setExtraParam('UUID', node.config.node.data["UUID"]);
                           
                        }
                     
                    }
                },
                checkchange: function (a, b, c, d) {
                    var oUuid = a.data.UUID;
                    if (a.data.checked == true) {
                        /*表加入*/
                        WS.SiteMapAction.setSiteMapIsActive(oUuid, "1", function (ret) {
                            if (ret.success == false) {
                                Ext.MessageBox.show({
                                    title: 'WARNING',
                                    msg: "ok",
                                    icon: Ext.MessageBox.WARNING,
                                    buttons: Ext.Msg.OK
                                });
                            }
                        });
                    } else {
                        WS.SiteMapAction.setSiteMapIsActive(oUuid, "0", function (ret) {
                            if (ret.success == false) {
                                Ext.MessageBox.show({
                                    title: 'WARNING',
                                    msg: "ok",
                                    icon: Ext.MessageBox.WARNING,
                                    buttons: Ext.Msg.OK
                                });
                            }
                        });

                    }
                }
            }
        }]
    });
    AppPageQuery.render('divMain');
});
</script>
			
			<div id="divMain" style="margin-bottom:5px;margin-top:35px;"></div>
<!-- 使用者session的檢查操作，當逾期時自動轉頁至登入頁面 -->
<script type="text/javascript" src='<%= Page.ResolveUrl("~/pageJs/keeySession.js")%>'></script>           
</asp:Content>
