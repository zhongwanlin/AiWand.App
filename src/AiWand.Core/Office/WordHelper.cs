using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace AiWand.Core.Office
{
    public class WordHelper
    {
        public static XWPFDocument Createm_DocxPage()
        {
            XWPFDocument m_Docx = new XWPFDocument();
            //页面设置
            m_Docx.Document.body.sectPr = new CT_SectPr();

            CT_SectPr m_SectPr = m_Docx.Document.body.sectPr;

            //创建页脚
            CT_Ftr m_ftr = new CT_Ftr();

            m_ftr.Items = new System.Collections.ArrayList();

            CT_SdtBlock m_Sdt = new CT_SdtBlock();

            CT_SdtPr m_SdtPr = m_Sdt.AddNewSdtPr();

            CT_SdtDocPart m_SdtDocPartObj = m_SdtPr.AddNewDocPartObj();

            m_SdtDocPartObj.AddNewDocPartGallery().val = "PageNumbers (Bottom of Page)";

            m_SdtDocPartObj.docPartUnique = new CT_OnOff();

            CT_SdtContentBlock m_SdtContent = m_Sdt.AddNewSdtContent();
            CT_P m_SdtContentP = m_SdtContent.AddNewP();
            CT_PPr m_SdtContentPpPr = m_SdtContentP.AddNewPPr();

            m_SdtContentPpPr.AddNewJc().val = ST_Jc.center;
            m_SdtContentP.Items = new System.Collections.ArrayList();

            CT_SimpleField m_fldSimple = new CT_SimpleField();

            m_fldSimple.instr = " PAGE   \\*MERGEFORMAT ";
            m_SdtContentP.Items.Add(m_fldSimple);
            m_ftr.Items.Add(m_Sdt);

            //创建页脚关系（footern.xml）
            XWPFRelation Frelation = XWPFRelation.FOOTER;
            XWPFFooter m_f = (XWPFFooter)m_Docx.CreateRelationship(Frelation, XWPFFactory.GetInstance(), m_Docx.FooterList.Count + 1);

            //设置页脚
            m_f.SetHeaderFooter(m_ftr);

            CT_HdrFtrRef m_HdrFtr = m_SectPr.AddNewFooterReference();

            m_HdrFtr.type = ST_HdrFtr.@default;
            m_HdrFtr.id = m_f.GetPackageRelationship().Id;

            return m_Docx;
        }
    }
}
