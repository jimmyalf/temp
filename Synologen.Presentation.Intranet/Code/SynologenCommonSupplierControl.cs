using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using Spinit.Wpc.Base.Data;
using Spinit.Wpc.Member.Data;
using Spinit.Wpc.Utility.Core;
using File = Spinit.Wpc.Base.Data.File;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Code
{
	public abstract class SynologenCommonSupplierControl : SynologenUserControl
	{
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
            var location = new Location(Base.Business.Globals.ConnectionString);
            LocationRow = (LocationRow)location.GetLocation(LocationId);
            FileRepository = new File(Base.Business.Globals.ConnectionString);
		}

		protected File FileRepository { get; private set; }
		protected LocationRow LocationRow { get; private set; }

		protected virtual string GetThumbUrl(FileRow fleRow, FileInfo fileInfo, string urlname)
		{
			var extensionWithoutDot = fileInfo.Extension.Replace(".", "");
			if (Base.Business.Globals.ImageType.Contains(extensionWithoutDot))
			{
				return GetImageSrcImage(fileInfo, urlname);
			}

			if (Base.Business.Globals.MediaType.Contains(extensionWithoutDot))
			{
				return "/wpc/Member/img/video_icon.gif";
			}

			if (Base.Business.Globals.DocumentType.Contains(extensionWithoutDot))
			{
				return GetImageSrcForDocument(fileInfo);
			}
			return null;
		}

		protected virtual string GetFileDescription(FileRow fleRow)
		{
			if(fleRow == null || fleRow.Description == null) return null;
			var desc = fleRow.Description;
			if (desc.Length > 30)
			{
				desc = desc.Substring(0, 27);
				if (desc.LastIndexOf(" ") != -1)
				{
					desc = desc.Substring(0, desc.LastIndexOf(" "));
				}
				desc += "...";
			}
			return desc;
		}

		protected virtual string GetImageSrcForDocument(FileInfo fileInfo)
		{
            switch (fileInfo.Extension) {
                case ".pdf": return "/wpc/Synologen/Supplier/pdfthumbnail.aspx";
                case ".xls": return "/wpc/Synologen/Supplier/Images/excel.gif";
                case ".xlsx": return "/wpc/Synologen/Supplier/Images/exel.gif";
                case ".ppt": return "/wpc/Synologen/Supplier/Images/powerpoint.gif";
                case ".pptx": return "/wpc/Synologen/Supplier/Images/powerpoint.gif";
                case ".doc": return "/wpc/Synologen/Supplier/Images/word.gif";
                case ".docx": return "/wpc/Synologen/Supplier/Images/word.gif";
                case ".eps": return "/wpc/Synologen/Supplier/Images/eps.png";
                default: return "/wpc/Synologen/Supplier/Images/standard.png";
            }
		}
		
		protected virtual string GetImageSrcImage(FileInfo file, string urlname)
		{
		    var ext = file.Extension.Trim('.');
			return "{Url}?filename={FileName}&width={Width}&height={Height}&ext={Extension}"
				.Replace("{Url}", "/Wpc/Synologen/Supplier/ViewMemberImage.aspx")
				.Replace("{FileName}", HttpUtility.UrlEncode(urlname))
				.Replace("{Width}", "100")
				.Replace("{Height}", "100")
				.Replace("{Extension}", /*fleRow.ContentInfo.ToLower()*/ ext);
		}
		
		protected virtual IEnumerable<string> AllowedExtensions
		{
			get { return String.Concat(Base.Business.Globals.ImageType, ",", Base.Business.Globals.MediaType, ",", Base.Business.Globals.FlashType, ",", Base.Business.Globals.DocumentType).Split(new[] {','}); }
		}

		protected virtual DirectoryInfo GetMemberDirectory(LocationRow lrow, MemberRow memberRow, FileCategoryRow fileCategory = null)
		{
			return fileCategory != null 
				? GetMemberDirectory(lrow, memberRow, fileCategory.Name) 
				: GetMemberDirectory(lrow, memberRow, (string) null);
		}

		protected virtual DirectoryInfo GetMemberDirectory(LocationRow lrow, MemberRow memberRow, string fileCategory)
		{
			var orgName = UrlFriendlyRenamingService.Rename(memberRow.OrgName);
			if(fileCategory == null)
			{
				return new DirectoryInfo(Base.Business.Globals.CommonFilePath + lrow.Name + "\\Member\\" + orgName);
			}
			var category = UrlFriendlyRenamingService.Rename(fileCategory);
			return new DirectoryInfo(Base.Business.Globals.CommonFilePath + lrow.Name + "\\Member\\" + orgName + "\\" + category);
		}

        protected virtual string GetMemberBaseDirectory(IBaseLocationRow lrow, MemberRow memberRow)
        {
            var di = GetMemberDirectory(LocationRow, memberRow);
            if (!di.Exists)
            {
                di.Create();
            }

            var orgName = UrlFriendlyRenamingService.Rename(memberRow.OrgName);
            return string.Format("~{0}{1}/Member/{2}/", Base.Business.Globals.CommonFileUrl, lrow.Name, orgName);
        }
		
		protected virtual string GetFileUrl(LocationRow lrow, MemberRow memberRow, FileCategoryRow fileCategory, string fileName)
		{
			return GetFileUrl(lrow, memberRow, fileCategory.Name, fileName);
		}

		protected virtual string GetFileUrl(LocationRow lrow, MemberRow memberRow, string fileCategory, string fileName)
		{
			var orgName = UrlFriendlyRenamingService.Rename(memberRow.OrgName);
			var category = UrlFriendlyRenamingService.Rename(fileCategory);
			return  lrow.Name + "/Member/" + orgName + "/" + category + "/" + fileName;
		}


		protected virtual IEnumerable<FileInfo> GetMemberFiles(LocationRow lrow, MemberRow memberRow, FileCategoryRow fileCatRow)
		{
			return GetMemberDirectory(lrow, memberRow, fileCatRow)
				.GetFiles("*", SearchOption.TopDirectoryOnly);
		}		 
	}
}