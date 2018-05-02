namespace Mercator.GIS.Map.Browser
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.menuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.打开OToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemOpenShpAndTransform = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemClearLayers = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemPixelPoint = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAddPixelPoint = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAddReferencePoint = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemClearPixelPoints = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemClearReferencePoints = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemSaveCoordinates = new System.Windows.Forms.ToolStripMenuItem();
            this.制图DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDrawMarker = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDrawRoute = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDrawPolygon = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemExportSketch = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemExportTransformedSketch = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemClearSketchPoints = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemClearSketchPolylines = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemClearSketchPolygons = new System.Windows.Forms.ToolStripMenuItem();
            this.底图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.googleChinaHybridMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.googleChinaSatelliteMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.googleChinaMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.googleChinaTerrainMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
            this.bingMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bingHybridMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bingSatelliteMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemDownload = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemExport = new System.Windows.Forms.ToolStripMenuItem();
            this.属性AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemQueryAttribute = new System.Windows.Forms.ToolStripMenuItem();
            this.工具TToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xy2lbToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemView = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemLayerWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemPropertyWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemOutputWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemClearOutputText = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonOpenShpAndTransform = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAddPixelPoint = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAddReferencePoint = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonDrawMarker = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDrawRoute = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDrawPolygon = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonExportSketch = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonQueryAttribute = new System.Windows.Forms.ToolStripButton();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.dockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.menuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemFile,
            this.menuItemPixelPoint,
            this.制图DToolStripMenuItem,
            this.底图ToolStripMenuItem,
            this.属性AToolStripMenuItem,
            this.工具TToolStripMenuItem,
            this.menuItemView});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(737, 25);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // menuItemFile
            // 
            this.menuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开OToolStripMenuItem,
            this.menuItemOpenShpAndTransform,
            this.toolStripMenuItem5,
            this.menuItemClearLayers});
            this.menuItemFile.Name = "menuItemFile";
            this.menuItemFile.Size = new System.Drawing.Size(58, 21);
            this.menuItemFile.Text = "文件(&F)";
            // 
            // 打开OToolStripMenuItem
            // 
            this.打开OToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("打开OToolStripMenuItem.Image")));
            this.打开OToolStripMenuItem.Name = "打开OToolStripMenuItem";
            this.打开OToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.打开OToolStripMenuItem.Text = "打开(&O)";
            this.打开OToolStripMenuItem.Click += new System.EventHandler(this.OpenShpFile);
            // 
            // menuItemOpenShpAndTransform
            // 
            this.menuItemOpenShpAndTransform.Image = ((System.Drawing.Image)(resources.GetObject("menuItemOpenShpAndTransform.Image")));
            this.menuItemOpenShpAndTransform.Name = "menuItemOpenShpAndTransform";
            this.menuItemOpenShpAndTransform.Size = new System.Drawing.Size(151, 22);
            this.menuItemOpenShpAndTransform.Text = "打开并转换(&T)";
            this.menuItemOpenShpAndTransform.Click += new System.EventHandler(this.OpenShpFileAndTransform);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(148, 6);
            // 
            // menuItemClearLayers
            // 
            this.menuItemClearLayers.Name = "menuItemClearLayers";
            this.menuItemClearLayers.Size = new System.Drawing.Size(151, 22);
            this.menuItemClearLayers.Text = "清空图层(&L)";
            this.menuItemClearLayers.Click += new System.EventHandler(this.menuItemClearLayers_Click);
            // 
            // menuItemPixelPoint
            // 
            this.menuItemPixelPoint.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemAddPixelPoint,
            this.menuItemAddReferencePoint,
            this.toolStripMenuItem1,
            this.menuItemClearPixelPoints,
            this.menuItemClearReferencePoints,
            this.toolStripMenuItem2,
            this.menuItemSaveCoordinates});
            this.menuItemPixelPoint.Name = "menuItemPixelPoint";
            this.menuItemPixelPoint.Size = new System.Drawing.Size(59, 21);
            this.menuItemPixelPoint.Text = "像控(&P)";
            // 
            // menuItemAddPixelPoint
            // 
            this.menuItemAddPixelPoint.Image = ((System.Drawing.Image)(resources.GetObject("menuItemAddPixelPoint.Image")));
            this.menuItemAddPixelPoint.Name = "menuItemAddPixelPoint";
            this.menuItemAddPixelPoint.Size = new System.Drawing.Size(178, 22);
            this.menuItemAddPixelPoint.Text = "添加像控点(&C)";
            this.menuItemAddPixelPoint.Click += new System.EventHandler(this.menuItemAddPixelPoint_Click);
            // 
            // menuItemAddReferencePoint
            // 
            this.menuItemAddReferencePoint.Image = ((System.Drawing.Image)(resources.GetObject("menuItemAddReferencePoint.Image")));
            this.menuItemAddReferencePoint.Name = "menuItemAddReferencePoint";
            this.menuItemAddReferencePoint.Size = new System.Drawing.Size(178, 22);
            this.menuItemAddReferencePoint.Text = "添加参考点(&R)";
            this.menuItemAddReferencePoint.Click += new System.EventHandler(this.menuItemAddReferencePoint_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(175, 6);
            // 
            // menuItemClearPixelPoints
            // 
            this.menuItemClearPixelPoints.Name = "menuItemClearPixelPoints";
            this.menuItemClearPixelPoints.Size = new System.Drawing.Size(178, 22);
            this.menuItemClearPixelPoints.Text = "清空像控点(&D)";
            this.menuItemClearPixelPoints.Click += new System.EventHandler(this.menuItemClearPixelPoints_Click);
            // 
            // menuItemClearReferencePoints
            // 
            this.menuItemClearReferencePoints.Name = "menuItemClearReferencePoints";
            this.menuItemClearReferencePoints.Size = new System.Drawing.Size(178, 22);
            this.menuItemClearReferencePoints.Text = "清空参考点(&P)";
            this.menuItemClearReferencePoints.Click += new System.EventHandler(this.menuItemClearReferencePoints_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(175, 6);
            // 
            // menuItemSaveCoordinates
            // 
            this.menuItemSaveCoordinates.Image = ((System.Drawing.Image)(resources.GetObject("menuItemSaveCoordinates.Image")));
            this.menuItemSaveCoordinates.Name = "menuItemSaveCoordinates";
            this.menuItemSaveCoordinates.Size = new System.Drawing.Size(178, 22);
            this.menuItemSaveCoordinates.Text = "保存控制点坐标(&O)";
            this.menuItemSaveCoordinates.Click += new System.EventHandler(this.menuItemSaveCoordinates_Click);
            // 
            // 制图DToolStripMenuItem
            // 
            this.制图DToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemDrawMarker,
            this.menuItemDrawRoute,
            this.menuItemDrawPolygon,
            this.toolStripMenuItem3,
            this.menuItemExportSketch,
            this.menuItemExportTransformedSketch,
            this.toolStripMenuItem6,
            this.menuItemClearSketchPoints,
            this.menuItemClearSketchPolylines,
            this.menuItemClearSketchPolygons});
            this.制图DToolStripMenuItem.Name = "制图DToolStripMenuItem";
            this.制图DToolStripMenuItem.Size = new System.Drawing.Size(61, 21);
            this.制图DToolStripMenuItem.Text = "草图(&D)";
            // 
            // menuItemDrawMarker
            // 
            this.menuItemDrawMarker.Image = ((System.Drawing.Image)(resources.GetObject("menuItemDrawMarker.Image")));
            this.menuItemDrawMarker.Name = "menuItemDrawMarker";
            this.menuItemDrawMarker.Size = new System.Drawing.Size(175, 22);
            this.menuItemDrawMarker.Text = "绘点 - 标记(&M)";
            this.menuItemDrawMarker.Click += new System.EventHandler(this.menuItemDrawMarker_Click);
            // 
            // menuItemDrawRoute
            // 
            this.menuItemDrawRoute.Image = ((System.Drawing.Image)(resources.GetObject("menuItemDrawRoute.Image")));
            this.menuItemDrawRoute.Name = "menuItemDrawRoute";
            this.menuItemDrawRoute.Size = new System.Drawing.Size(175, 22);
            this.menuItemDrawRoute.Text = "绘线 - 路径(&R)";
            this.menuItemDrawRoute.Click += new System.EventHandler(this.menuItemDrawRoute_Click);
            // 
            // menuItemDrawPolygon
            // 
            this.menuItemDrawPolygon.Image = ((System.Drawing.Image)(resources.GetObject("menuItemDrawPolygon.Image")));
            this.menuItemDrawPolygon.Name = "menuItemDrawPolygon";
            this.menuItemDrawPolygon.Size = new System.Drawing.Size(175, 22);
            this.menuItemDrawPolygon.Text = "绘面 - 多边形(&P)";
            this.menuItemDrawPolygon.Click += new System.EventHandler(this.menuItemDrawPolygon_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(172, 6);
            // 
            // menuItemExportSketch
            // 
            this.menuItemExportSketch.Image = ((System.Drawing.Image)(resources.GetObject("menuItemExportSketch.Image")));
            this.menuItemExportSketch.Name = "menuItemExportSketch";
            this.menuItemExportSketch.Size = new System.Drawing.Size(175, 22);
            this.menuItemExportSketch.Text = "保存草图(&E)";
            this.menuItemExportSketch.Click += new System.EventHandler(this.menuItemExportSketch_Click);
            // 
            // menuItemExportTransformedSketch
            // 
            this.menuItemExportTransformedSketch.Image = ((System.Drawing.Image)(resources.GetObject("menuItemExportTransformedSketch.Image")));
            this.menuItemExportTransformedSketch.Name = "menuItemExportTransformedSketch";
            this.menuItemExportTransformedSketch.Size = new System.Drawing.Size(175, 22);
            this.menuItemExportTransformedSketch.Text = "保存草图并转换(&S)";
            this.menuItemExportTransformedSketch.Click += new System.EventHandler(this.menuItemExportTransformedSketch_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(172, 6);
            // 
            // menuItemClearSketchPoints
            // 
            this.menuItemClearSketchPoints.Name = "menuItemClearSketchPoints";
            this.menuItemClearSketchPoints.Size = new System.Drawing.Size(175, 22);
            this.menuItemClearSketchPoints.Text = "清除草图点(&I)";
            this.menuItemClearSketchPoints.Click += new System.EventHandler(this.menuItemClearSketchPoints_Click);
            // 
            // menuItemClearSketchPolylines
            // 
            this.menuItemClearSketchPolylines.Name = "menuItemClearSketchPolylines";
            this.menuItemClearSketchPolylines.Size = new System.Drawing.Size(175, 22);
            this.menuItemClearSketchPolylines.Text = "清除草图线(&L)";
            this.menuItemClearSketchPolylines.Click += new System.EventHandler(this.menuItemClearSketchPolylines_Click);
            // 
            // menuItemClearSketchPolygons
            // 
            this.menuItemClearSketchPolygons.Name = "menuItemClearSketchPolygons";
            this.menuItemClearSketchPolygons.Size = new System.Drawing.Size(175, 22);
            this.menuItemClearSketchPolygons.Text = "清除草图面(&A)";
            this.menuItemClearSketchPolygons.Click += new System.EventHandler(this.menuItemClearSketchPolygons_Click);
            // 
            // 底图ToolStripMenuItem
            // 
            this.底图ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.googleChinaHybridMapToolStripMenuItem,
            this.googleChinaSatelliteMapToolStripMenuItem,
            this.googleChinaMapToolStripMenuItem,
            this.googleChinaTerrainMapToolStripMenuItem,
            this.toolStripMenuItem7,
            this.bingMapToolStripMenuItem,
            this.bingHybridMapToolStripMenuItem,
            this.bingSatelliteMapToolStripMenuItem,
            this.toolStripMenuItem8,
            this.menuItemDownload,
            this.menuItemExport});
            this.底图ToolStripMenuItem.Name = "底图ToolStripMenuItem";
            this.底图ToolStripMenuItem.Size = new System.Drawing.Size(64, 21);
            this.底图ToolStripMenuItem.Text = "底图(&M)";
            // 
            // googleChinaHybridMapToolStripMenuItem
            // 
            this.googleChinaHybridMapToolStripMenuItem.Name = "googleChinaHybridMapToolStripMenuItem";
            this.googleChinaHybridMapToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.googleChinaHybridMapToolStripMenuItem.Text = "GoogleChinaHybridMap";
            this.googleChinaHybridMapToolStripMenuItem.Click += new System.EventHandler(this.ChangeMapSource);
            // 
            // googleChinaSatelliteMapToolStripMenuItem
            // 
            this.googleChinaSatelliteMapToolStripMenuItem.Name = "googleChinaSatelliteMapToolStripMenuItem";
            this.googleChinaSatelliteMapToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.googleChinaSatelliteMapToolStripMenuItem.Text = "GoogleChinaSatelliteMap";
            this.googleChinaSatelliteMapToolStripMenuItem.Click += new System.EventHandler(this.ChangeMapSource);
            // 
            // googleChinaMapToolStripMenuItem
            // 
            this.googleChinaMapToolStripMenuItem.Name = "googleChinaMapToolStripMenuItem";
            this.googleChinaMapToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.googleChinaMapToolStripMenuItem.Text = "GoogleChinaMap";
            this.googleChinaMapToolStripMenuItem.Click += new System.EventHandler(this.ChangeMapSource);
            // 
            // googleChinaTerrainMapToolStripMenuItem
            // 
            this.googleChinaTerrainMapToolStripMenuItem.Name = "googleChinaTerrainMapToolStripMenuItem";
            this.googleChinaTerrainMapToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.googleChinaTerrainMapToolStripMenuItem.Text = "GoogleChinaTerrainMap";
            this.googleChinaTerrainMapToolStripMenuItem.Click += new System.EventHandler(this.ChangeMapSource);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(220, 6);
            // 
            // bingMapToolStripMenuItem
            // 
            this.bingMapToolStripMenuItem.Name = "bingMapToolStripMenuItem";
            this.bingMapToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.bingMapToolStripMenuItem.Text = "BingMap";
            this.bingMapToolStripMenuItem.Click += new System.EventHandler(this.ChangeMapSource);
            // 
            // bingHybridMapToolStripMenuItem
            // 
            this.bingHybridMapToolStripMenuItem.Name = "bingHybridMapToolStripMenuItem";
            this.bingHybridMapToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.bingHybridMapToolStripMenuItem.Text = "BingHybridMap";
            this.bingHybridMapToolStripMenuItem.Click += new System.EventHandler(this.ChangeMapSource);
            // 
            // bingSatelliteMapToolStripMenuItem
            // 
            this.bingSatelliteMapToolStripMenuItem.Name = "bingSatelliteMapToolStripMenuItem";
            this.bingSatelliteMapToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.bingSatelliteMapToolStripMenuItem.Text = "BingSatelliteMap";
            this.bingSatelliteMapToolStripMenuItem.Click += new System.EventHandler(this.ChangeMapSource);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(220, 6);
            // 
            // menuItemDownload
            // 
            this.menuItemDownload.Name = "menuItemDownload";
            this.menuItemDownload.Size = new System.Drawing.Size(223, 22);
            this.menuItemDownload.Text = "下载离线地图(&O)";
            this.menuItemDownload.Click += new System.EventHandler(this.menuItemDownload_Click);
            // 
            // menuItemExport
            // 
            this.menuItemExport.Name = "menuItemExport";
            this.menuItemExport.Size = new System.Drawing.Size(223, 22);
            this.menuItemExport.Text = "导出离线地图(&A)";
            this.menuItemExport.Click += new System.EventHandler(this.menuItemExport_Click);
            // 
            // 属性AToolStripMenuItem
            // 
            this.属性AToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemQueryAttribute});
            this.属性AToolStripMenuItem.Name = "属性AToolStripMenuItem";
            this.属性AToolStripMenuItem.Size = new System.Drawing.Size(60, 21);
            this.属性AToolStripMenuItem.Text = "属性(&A)";
            // 
            // menuItemQueryAttribute
            // 
            this.menuItemQueryAttribute.Image = ((System.Drawing.Image)(resources.GetObject("menuItemQueryAttribute.Image")));
            this.menuItemQueryAttribute.Name = "menuItemQueryAttribute";
            this.menuItemQueryAttribute.Size = new System.Drawing.Size(180, 22);
            this.menuItemQueryAttribute.Text = "查询实体属性(&E)";
            this.menuItemQueryAttribute.Click += new System.EventHandler(this.menuItemQueryAttribute_Click);
            // 
            // 工具TToolStripMenuItem
            // 
            this.工具TToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xy2lbToolStripMenuItem});
            this.工具TToolStripMenuItem.Name = "工具TToolStripMenuItem";
            this.工具TToolStripMenuItem.Size = new System.Drawing.Size(59, 21);
            this.工具TToolStripMenuItem.Text = "工具(&T)";
            // 
            // xy2lbToolStripMenuItem
            // 
            this.xy2lbToolStripMenuItem.Name = "xy2lbToolStripMenuItem";
            this.xy2lbToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.xy2lbToolStripMenuItem.Text = "西安80坐标转经纬度(&X)";
            this.xy2lbToolStripMenuItem.Click += new System.EventHandler(this.xy2lbToolStripMenuItem_Click);
            // 
            // menuItemView
            // 
            this.menuItemView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemLayerWindow,
            this.menuItemPropertyWindow,
            this.menuItemOutputWindow,
            this.toolStripMenuItem4,
            this.menuItemClearOutputText});
            this.menuItemView.Name = "menuItemView";
            this.menuItemView.Size = new System.Drawing.Size(60, 21);
            this.menuItemView.Text = "视图(&V)";
            // 
            // menuItemLayerWindow
            // 
            this.menuItemLayerWindow.Name = "menuItemLayerWindow";
            this.menuItemLayerWindow.Size = new System.Drawing.Size(188, 22);
            this.menuItemLayerWindow.Text = "图层管理器(&L)";
            this.menuItemLayerWindow.Click += new System.EventHandler(this.menuItemLayerWindow_Click);
            // 
            // menuItemPropertyWindow
            // 
            this.menuItemPropertyWindow.Name = "menuItemPropertyWindow";
            this.menuItemPropertyWindow.Size = new System.Drawing.Size(188, 22);
            this.menuItemPropertyWindow.Text = "属性窗口(&W)";
            this.menuItemPropertyWindow.Click += new System.EventHandler(this.menuItemPropertyWindow_Click);
            // 
            // menuItemOutputWindow
            // 
            this.menuItemOutputWindow.Name = "menuItemOutputWindow";
            this.menuItemOutputWindow.Size = new System.Drawing.Size(188, 22);
            this.menuItemOutputWindow.Text = "输出窗口(&O)";
            this.menuItemOutputWindow.Click += new System.EventHandler(this.menuItemOutput_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(185, 6);
            // 
            // menuItemClearOutputText
            // 
            this.menuItemClearOutputText.Name = "menuItemClearOutputText";
            this.menuItemClearOutputText.Size = new System.Drawing.Size(188, 22);
            this.menuItemClearOutputText.Text = "清空输出窗口内容(&C)";
            this.menuItemClearOutputText.Click += new System.EventHandler(this.menuItemClearOutputText_Click);
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonOpenShpAndTransform,
            this.toolStripSeparator1,
            this.toolStripButton1,
            this.toolStripButtonAddPixelPoint,
            this.toolStripButtonAddReferencePoint,
            this.toolStripButton2,
            this.toolStripSeparator2,
            this.toolStripButtonDrawMarker,
            this.toolStripButtonDrawRoute,
            this.toolStripButtonDrawPolygon,
            this.toolStripButtonExportSketch,
            this.toolStripButton3,
            this.toolStripSeparator3,
            this.toolStripButtonQueryAttribute});
            this.toolStrip.Location = new System.Drawing.Point(0, 25);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(737, 25);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip";
            // 
            // toolStripButtonOpenShpAndTransform
            // 
            this.toolStripButtonOpenShpAndTransform.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonOpenShpAndTransform.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonOpenShpAndTransform.Image")));
            this.toolStripButtonOpenShpAndTransform.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOpenShpAndTransform.Name = "toolStripButtonOpenShpAndTransform";
            this.toolStripButtonOpenShpAndTransform.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonOpenShpAndTransform.Text = "打开并转换(&T)";
            this.toolStripButtonOpenShpAndTransform.Click += new System.EventHandler(this.OpenShpFileAndTransform);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "打开(&O)";
            this.toolStripButton1.Click += new System.EventHandler(this.OpenShpFile);
            // 
            // toolStripButtonAddPixelPoint
            // 
            this.toolStripButtonAddPixelPoint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAddPixelPoint.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAddPixelPoint.Image")));
            this.toolStripButtonAddPixelPoint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAddPixelPoint.Name = "toolStripButtonAddPixelPoint";
            this.toolStripButtonAddPixelPoint.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonAddPixelPoint.Text = "添加像控点(&C)";
            this.toolStripButtonAddPixelPoint.Click += new System.EventHandler(this.menuItemAddPixelPoint_Click);
            // 
            // toolStripButtonAddReferencePoint
            // 
            this.toolStripButtonAddReferencePoint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAddReferencePoint.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAddReferencePoint.Image")));
            this.toolStripButtonAddReferencePoint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAddReferencePoint.Name = "toolStripButtonAddReferencePoint";
            this.toolStripButtonAddReferencePoint.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonAddReferencePoint.Text = "添加参考点(&R)";
            this.toolStripButtonAddReferencePoint.Click += new System.EventHandler(this.menuItemAddReferencePoint_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "保存控制点坐标(&O)";
            this.toolStripButton2.Click += new System.EventHandler(this.menuItemSaveCoordinates_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonDrawMarker
            // 
            this.toolStripButtonDrawMarker.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonDrawMarker.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDrawMarker.Image")));
            this.toolStripButtonDrawMarker.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDrawMarker.Name = "toolStripButtonDrawMarker";
            this.toolStripButtonDrawMarker.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonDrawMarker.Text = "绘点 - 标记(&M)";
            this.toolStripButtonDrawMarker.Click += new System.EventHandler(this.menuItemDrawMarker_Click);
            // 
            // toolStripButtonDrawRoute
            // 
            this.toolStripButtonDrawRoute.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonDrawRoute.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDrawRoute.Image")));
            this.toolStripButtonDrawRoute.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDrawRoute.Name = "toolStripButtonDrawRoute";
            this.toolStripButtonDrawRoute.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonDrawRoute.Text = "绘线 - 路径(&R)";
            this.toolStripButtonDrawRoute.Click += new System.EventHandler(this.menuItemDrawRoute_Click);
            // 
            // toolStripButtonDrawPolygon
            // 
            this.toolStripButtonDrawPolygon.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonDrawPolygon.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDrawPolygon.Image")));
            this.toolStripButtonDrawPolygon.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDrawPolygon.Name = "toolStripButtonDrawPolygon";
            this.toolStripButtonDrawPolygon.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonDrawPolygon.Text = "绘面 - 多边形(&P)";
            this.toolStripButtonDrawPolygon.Click += new System.EventHandler(this.menuItemDrawPolygon_Click);
            // 
            // toolStripButtonExportSketch
            // 
            this.toolStripButtonExportSketch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonExportSketch.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonExportSketch.Image")));
            this.toolStripButtonExportSketch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonExportSketch.Name = "toolStripButtonExportSketch";
            this.toolStripButtonExportSketch.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonExportSketch.Text = "保存草图(&E)";
            this.toolStripButtonExportSketch.Click += new System.EventHandler(this.menuItemExportSketch_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "保存草图并转换(&S)";
            this.toolStripButton3.Click += new System.EventHandler(this.menuItemExportTransformedSketch_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonQueryAttribute
            // 
            this.toolStripButtonQueryAttribute.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonQueryAttribute.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonQueryAttribute.Image")));
            this.toolStripButtonQueryAttribute.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonQueryAttribute.Name = "toolStripButtonQueryAttribute";
            this.toolStripButtonQueryAttribute.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonQueryAttribute.Text = "查询实体属性(&E)";
            this.toolStripButtonQueryAttribute.Click += new System.EventHandler(this.menuItemQueryAttribute_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Location = new System.Drawing.Point(0, 514);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(737, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip";
            // 
            // dockPanel
            // 
            this.dockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingSdi;
            this.dockPanel.Location = new System.Drawing.Point(0, 50);
            this.dockPanel.Name = "dockPanel";
            this.dockPanel.Size = new System.Drawing.Size(737, 464);
            this.dockPanel.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 536);
            this.Controls.Add(this.dockPanel);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.Text = "地图浏览器";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripMenuItem menuItemFile;
        private System.Windows.Forms.ToolStripMenuItem menuItemOpenShpAndTransform;
        private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel;
        private System.Windows.Forms.ToolStripButton toolStripButtonOpenShpAndTransform;
        private System.Windows.Forms.ToolStripMenuItem menuItemView;
        private System.Windows.Forms.ToolStripMenuItem menuItemLayerWindow;
        private System.Windows.Forms.ToolStripMenuItem menuItemPropertyWindow;
        private System.Windows.Forms.ToolStripMenuItem menuItemPixelPoint;
        private System.Windows.Forms.ToolStripMenuItem menuItemAddPixelPoint;
        private System.Windows.Forms.ToolStripMenuItem menuItemOutputWindow;
        private System.Windows.Forms.ToolStripMenuItem menuItemAddReferencePoint;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem menuItemClearPixelPoints;
        private System.Windows.Forms.ToolStripMenuItem menuItemClearReferencePoints;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonAddPixelPoint;
        private System.Windows.Forms.ToolStripButton toolStripButtonAddReferencePoint;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem menuItemSaveCoordinates;
        private System.Windows.Forms.ToolStripMenuItem 打开OToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripMenuItem 制图DToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuItemDrawMarker;
        private System.Windows.Forms.ToolStripMenuItem menuItemDrawRoute;
        private System.Windows.Forms.ToolStripMenuItem menuItemDrawPolygon;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButtonDrawMarker;
        private System.Windows.Forms.ToolStripButton toolStripButtonDrawRoute;
        private System.Windows.Forms.ToolStripButton toolStripButtonDrawPolygon;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem menuItemExportSketch;
        private System.Windows.Forms.ToolStripButton toolStripButtonExportSketch;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem menuItemClearOutputText;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem menuItemClearLayers;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem menuItemClearSketchPoints;
        private System.Windows.Forms.ToolStripMenuItem menuItemClearSketchPolylines;
        private System.Windows.Forms.ToolStripMenuItem menuItemClearSketchPolygons;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripMenuItem 底图ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem googleChinaHybridMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem googleChinaSatelliteMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem googleChinaMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem googleChinaTerrainMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem bingMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bingHybridMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bingSatelliteMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuItemExportTransformedSketch;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripMenuItem 属性AToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuItemQueryAttribute;
        private System.Windows.Forms.ToolStripButton toolStripButtonQueryAttribute;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem menuItemDownload;
        private System.Windows.Forms.ToolStripMenuItem menuItemExport;
        private System.Windows.Forms.ToolStripMenuItem 工具TToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xy2lbToolStripMenuItem;
    }
}