Ext.Loader.setConfig({
    enabled: true
});
Ext.Loader.setPath('Ext.ux', SYSTEM_ROOT_PATH + '/js/ux');
Ext.require(['*', 'Ext.ux.DataTip']);
Ext.MessageBox.buttonText.yes = "確定";
Ext.MessageBox.buttonText.no = "取消";
Ext.onReady(function() {
    Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".AttendantAction"));
    Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".UploadJobAction"));
    Ext.direct.Manager.addProvider(eval(SYSTEM_APPLICATION + ".CalAction"));
    Ext.define('Cal', {
        extend: 'Ext.window.Window',
        title: '啟動計算',
        closeAction: 'hide',
        uuid: undefined,
        companyUuid: undefined,
        width: 700,
        height: 600,
        layout: 'fit',
        resizable: false,

        draggable: true,
        initComponent: function() {
            var me = this;
            me.items = [Ext.create('Ext.form.Panel', {
                layout: {
                    type: 'form',
                    align: 'stretch'
                },
                api: {
                    load: WS.AttendantAction.info,
                    submit: WS.AttendantAction.submit
                },
                paramOrder: ['pUuid'],
                border: true,
                autoScroll: false,
                defaultType: 'textfield',
                buttonAlign: 'center',
                items: [{
                    xtype: 'container',
                    layout: 'hbox',
                    margin:'5 5 5 5',
                    items: [{
                        xtype: 'combo',
                        fieldLabel: '年度客戶',
                        labelAlign: 'right',
                        margin: '0 5 0 0',
                        itemId: 'cmbFrameHead1',
                        displayField: 'C_NAME',
                        valueField: 'UUID',
                        emptyText: '年份',
                        tpl: new Ext.XTemplate(
                            '<tpl for=".">',
                            '<li role="option" unselectable="on" class="x-boundlist-item">',
                            '{[this.showName(values.FULL_FRAME_NAME_LIST)]}',
                            '</li>',
                            '</tpl>', {
                                showName: function(fullName) {
                                    var arrName = fullName.split(':');
                                    var ret = "";
                                    if (arrName.length > 1) {
                                        for (var i = 1; i < arrName.length; i++) {
                                            if (i == (arrName.length - 2)) {
                                                ret += arrName[i];
                                            } else {
                                                //ret += "&nbsp;&nbsp;&nbsp;&nbsp;";
                                            }
                                        }
                                    }
                                    return ret;
                                }
                            }
                        ),
                        editable: false,
                        hidden: false,
                        store: Ext.create('Ext.data.Store', {
                            extend: 'Ext.data.Store',
                            remoteSort: false,
                            autoLoad: false,
                            model: 'FRAME_HEAD',
                            pageSize: 9999,
                            proxy: {
                                type: 'direct',
                                api: {
                                    read: WS.FrameAction.loadFrameHeadCmb1
                                },
                                reader: {
                                    root: 'data'
                                },
                                paramsAsHash: true,
                                paramOrder: ['pCompanyUuid', 'pageNo', 'limitNo', 'sort', 'dir'],
                                extraParams: {
                                    'pCompanyUuid': '',
                                    'pageNo': '',
                                    'limitNo': '',
                                    'sort': '',
                                    'dir': ''
                                },
                                simpleSortMode: true
                            }
                        }),
                        listeners: {
                            'select': function(combo, records, eOpts) {
                                combo.up('panel').down('#cmbFrameHead2').getStore().getProxy().setExtraParam('parent_frame_head_uuid', combo.getValue());
                                combo.up('panel').down('#cmbFrameHead2').getStore().getProxy().setExtraParam('frame_category_uuid', '');

                                combo.up('panel').down('#cmbFrameHead2').getStore().reload({
                                    callback: function() {
                                        this.down('#cmbFrameHead2').setValue('');
                                    },
                                    scope: combo.up('panel')
                                });
                            }
                        }
                    }, {
                        xtype: 'combo',
                        labelAlign: 'right',
                        width: 370,
                        fieldLabel: '公司',
                        margin: '0 5 0 0',
                        emptyText: '公司',
                        autoLoad: false,
                        itemId: 'cmbFrameHead2',
                        displayField: 'C_NAME',
                        valueField: 'UUID',
                        tpl: new Ext.XTemplate(
                            '<tpl for=".">',
                            '<li role="option" unselectable="on" class="x-boundlist-item">',
                            '{[this.showName(values.FULL_FRAME_NAME_LIST)]}',
                            '</li>',
                            '</tpl>', {
                                showName: function(fullName) {
                                    var arrName = fullName.split(':');
                                    var ret = "";
                                    if (arrName.length > 1) {
                                        for (var i = 1; i < arrName.length; i++) {
                                            if (i == (arrName.length - 2)) {
                                                ret += arrName[i];
                                            } else {
                                                //ret += "&nbsp;&nbsp;&nbsp;&nbsp;";
                                            }
                                        }
                                    }
                                    return ret;
                                }
                            }
                        ),
                        editable: false,
                        hidden: false,
                        store: Ext.create('Ext.data.Store', {
                            extend: 'Ext.data.Store',
                            remoteSort: false,
                            autoLoad: false,
                            model: 'FRAME_HEAD',
                            pageSize: 9999,
                            proxy: {
                                type: 'direct',
                                api: {
                                    read: WS.FrameAction.loadFrameHeadCmb2
                                },
                                reader: {
                                    root: 'data'
                                },
                                paramsAsHash: true,
                                paramOrder: ['parent_frame_head_uuid', 'frame_category_uuid', 'pageNo', 'limitNo', 'sort', 'dir'],
                                extraParams: {
                                    'parent_frame_head_uuid': '',
                                    'frame_category_uuid': '',
                                    'pageNo': '',
                                    'limitNo': '',
                                    'sort': '',
                                    'dir': ''
                                },
                                simpleSortMode: true
                            }
                        }),
                        listeners: {
                            'select': function(combo, records, eOpts) {
                                combo.up('window').down('#grdFrameHead').getStore().getProxy().setExtraParam('pParentFrameHeadUuid', combo.getValue());
                                combo.up('window').down('#grdFrameHead').getStore().loadPage(1);
                            }
                        }
                    }]
                }, {
                    xtype: 'container',
                    layout: 'vbox',
                    defaultType: 'textfield',
                    margin:'5 5 0 5',
                    items: [{
                        xtype: 'combo',
                        fieldLabel: '時間屬性',
                        labelAlign: 'right',
                        queryMode: 'local',
                        itemId: 'cmbTimeType',
                        displayField: 'text',
                        valueField: 'value',
                        editable: false,
                        hidden: false,
                        value: 'month',
                        store: new Ext.data.ArrayStore({
                            fields: ['text', 'value'],
                            data: [
                                ['月', 'month'],
                                ['年', 'year']
                            ]
                        }),
                        listeners: {
                            'select': function(combo, records, eOpts) {
                                combo.up('window').down("#cmbTimeIdStart").setValue('');
                                combo.up('window').down("#cmbTimeIdStart").getStore().getProxy().setExtraParam('pTimeType', combo.up('window').down("#cmbTimeType").getValue());
                                combo.up('window').down("#cmbTimeIdStart").getStore().reload();
                                combo.up('window').down("#cmbTimeIdEnd").setValue('');
                                combo.up('window').down("#cmbTimeIdEnd").getStore().getProxy().setExtraParam('pTimeType', combo.up('window').down("#cmbTimeType").getValue());
                                combo.up('window').down("#cmbTimeIdEnd").getStore().reload();
                            }
                        }
                    }]
                }, {
                    xtype: 'container',
                    layout: 'hbox',
                    margin:'0 5 5 5',
                    defaultType: 'textfield',
                    items: [{
                        xtype: 'combo',
                        fieldLabel: '報告期間(起)',
                        queryMode: 'local',
                        itemId: 'cmbTimeIdStart',
                        displayField: 'TIME_ID',
                        valueField: 'TIME_ID',
                        labelAlign: 'right',
                        editable: false,
                        hidden: false,
                        store: Ext.create('Ext.data.Store', {
                            extend: 'Ext.data.Store',
                            autoLoad: false,
                            remoteSort: true,
                            model: 'TIME',
                            pageSize: 9999,
                            proxy: {
                                type: 'direct',
                                api: {
                                    read: WS.TimeAction.loadTime
                                },
                                reader: {
                                    root: 'data'
                                },
                                paramsAsHash: true,
                                paramOrder: ['pTimeType', 'page', 'limit', 'sort', 'dir'],
                                extraParams: {
                                    'pTimeType': ''
                                },
                                simpleSortMode: true,
                                listeners: {
                                    exception: function(proxy, response, operation) {
                                        Ext.MessageBox.show({
                                            title: 'REMOTE EXCEPTON A',
                                            msg: operation.getError(),
                                            icon: Ext.MessageBox.ERROR,
                                            buttons: Ext.Msg.OK
                                        });
                                    },
                                    beforeload: function() {
                                        alert('beforeload proxy');
                                    }
                                }
                            },
                            sorters: [{
                                property: 'TIME_ID',
                                direction: 'ASC'
                            }]
                        })
                    }, {
                        xtype: 'combo',
                        fieldLabel: '報告期間(迄)',
                        queryMode: 'local',
                        itemId: 'cmbTimeIdEnd',
                        displayField: 'TIME_ID',
                        valueField: 'TIME_ID',
                        labelAlign: 'right',
                        editable: false,
                        hidden: false,
                        store: Ext.create('Ext.data.Store', {
                            extend: 'Ext.data.Store',
                            autoLoad: false,
                            remoteSort: true,
                            model: 'TIME',
                            pageSize: 9999,
                            proxy: {
                                type: 'direct',
                                api: {
                                    read: WS.TimeAction.loadTime
                                },
                                reader: {
                                    root: 'data'
                                },
                                paramsAsHash: true,
                                paramOrder: ['pTimeType', 'page', 'limit', 'sort', 'dir'],
                                extraParams: {
                                    'pTimeType': ''
                                },
                                simpleSortMode: true,
                                listeners: {
                                    exception: function(proxy, response, operation) {
                                        Ext.MessageBox.show({
                                            title: 'REMOTE EXCEPTON A',
                                            msg: operation.getError(),
                                            icon: Ext.MessageBox.ERROR,
                                            buttons: Ext.Msg.OK
                                        });
                                    },
                                    beforeload: function() {
                                        alert('beforeload proxy');
                                    }
                                }
                            },
                            sorters: [{
                                property: 'TIME_ID',
                                direction: 'ASC'
                            }]
                        })
                    }]
                }, {
                    xtype: 'fieldset',
                    title: '指定預計算的組織單位',
                    border: true,
                    width:650,
                    height:400,
                    items: [{
                        xtype: 'gridpanel',
                        itemId: 'grdFrameHead',
                        // selModel: new Ext.selection.CheckboxModel({
                        //     checkOnly: true
                        // }),
                        store: Ext.create('Ext.data.Store', {
                            extend: 'Ext.data.Store',
                            remoteSort: false,
                            autoLoad: false,
                            model: 'FRAME_HEAD',
                            pageSize: 9999,
                            proxy: {
                                type: 'direct',
                                api: {
                                    read: WS.FrameAction.loadFrameHeadCmb3
                                },
                                reader: {
                                    root: 'data'
                                },
                                paramsAsHash: true,
                                paramOrder: ['pParentFrameHeadUuid'],
                                extraParams: {
                                    'pParentFrameHeadUuid': ''
                                },
                                simpleSortMode: true
                            }
                        }),
                        paramsAsHash: false,
                        padding: 5,
                        autoScroll: true,
                        columns: [{
                            text: "將計算的組織範圍",
                            dataIndex: 'FULL_FRAME_NAME_LIST',
                            align: 'left',
                            flex: 3,
                            renderer: function(value, r) {
                                var ret = "";
                                var dlevel = parseInt(r.record.data["DLEVEL"]);
                                for (var i = 4; i < dlevel; i++) {
                                    ret += "&nbsp;&nbsp;&nbsp;&nbsp;";
                                }
                                var arrName = value.split(':');
                                ret += arrName[arrName.length - 2];
                                return ret;
                            }
                        }],
                        height: 400
                    }]
                }],
                fbar: [{
                    type: 'button',
                    text: '<img src="' + SYSTEM_URL_ROOT + '/css/images/save.gif" style="width:16px;height:16px;vertical-align:middle;margin-right:5px;"/>' + '啟動計算',
                    handler: function() {
                        //pTimeType, startTimeId, endTimeId, arrFrameHeadUuid
                        var pTimeType = this.up('window').down("#cmbTimeType").getValue();
                        var startTimeId = this.up('window').down("#cmbTimeIdStart").getValue();
                        var endTimeId = this.up('window').down("#cmbTimeIdEnd").getValue();
                        var arrFrameHead = "";
                        if (pTimeType == "" || startTimeId == "" || startTimeId == undefined || endTimeId == "" || endTimeId == undefined) {
                            Ext.MessageBox.show({
                                title: '警示',
                                icon: Ext.MessageBox.INFO,
                                buttons: Ext.Msg.OK,
                                msg: '時間屬性、報告期間(起)、報告期間(迄)欄位必需選擇。'
                            });
                            return;
                        }
                        if (startTimeId > endTimeId) {
                            Ext.MessageBox.show({
                                title: '警示',
                                icon: Ext.MessageBox.INFO,
                                buttons: Ext.Msg.OK,
                                msg: '報告期間(起)必須小於等於報告期間(迄)。'
                            });
                            return;
                        }
                        if (pTimeType == "month") {
                            if (startTimeId.substring(0, 4) != endTimeId.substring(0, 4)) {
                                Ext.MessageBox.show({
                                    title: '警示',
                                    icon: Ext.MessageBox.INFO,
                                    buttons: Ext.Msg.OK,
                                    msg: '不可以跨年份啟動工作。'
                                });
                                return;
                            }
                        }
                        
                        var _arrFrameHead = this.up('window').down("#cmbFrameHead2").getValue();
                        if(_arrFrameHead.length>0){
                            arrFrameHead += ";";
                            WS.CalAction.startCal(arrFrameHead, startTimeId, endTimeId, function(obj, jsonObj) {});
                            Ext.MessageBox.show({
                                title: '操作提示',
                                icon: Ext.MessageBox.INFO,
                                buttons: Ext.Msg.OK,
                                msg: '計算請求已經送出。'
                            });
                        }
                        else {
                            Ext.MessageBox.show({
                                title: '警示',
                                icon: Ext.MessageBox.INFO,
                                buttons: Ext.Msg.OK,
                                msg: '組織尚未選擇。'
                            });
                            return;
                        }
                    }
                }, {
                    type: 'button',
                    text: '<img src="' + SYSTEM_URL_ROOT + '/css/images/leave.png" style="width:20px;height:16px;vertical-align:middle;margin-right:5px;"/>' + '關閉',
                    handler: function() {
                        this.up('window').hide();
                    }
                }]
            })]
            me.callParent(arguments);
        },
        closeEvent: function() {
            this.fireEvent('closeEvent', this);
        },
        listeners: {
            'show': function() {
                Ext.getBody().mask();
                this.down("#cmbTimeIdStart").getStore().getProxy().setExtraParam('pTimeType', this.down("#cmbTimeType").getValue());
                this.down("#cmbTimeIdStart").getStore().load();
                this.down("#cmbTimeIdEnd").getStore().getProxy().setExtraParam('pTimeType', this.down("#cmbTimeType").getValue());
                this.down("#cmbTimeIdEnd").getStore().load();
                this.down("#grdFrameHead").getStore().getProxy().setExtraParam('pCompanyUuid', this.companyUuid);
                this.down("#grdFrameHead").getStore().load();

                this.down("#cmbFrameHead1").getStore().getProxy().setExtraParam('pCompanyUuid', this.companyUuid);
                this.down("#cmbFrameHead1").getStore().load();



            },
            'hide': function() {
                Ext.getBody().unmask();
                this.companyUuid = undefined;
                this.closeEvent();
            }
        }
    });
});
