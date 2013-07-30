using System;
using System.Configuration;
using System.IO;
using NUnit.Framework;
using Spinit.Extensions;

namespace Synologen.LensSubscription.Autogiro.FileIO.Test.Helpers
{
	[TestFixture]
	public abstract class IOBase
	{
		protected const string ReadFilesRootPathKey = "ReadFilesRootPath";
		protected const string WriteOutputRootPathKey = "WriteOutputRootPath";

		protected IOBase()
		{
			ReadRootPath = ConfigurationManager.AppSettings[ReadFilesRootPathKey];
			WriteRootPath = ConfigurationManager.AppSettings[WriteOutputRootPathKey];
			Because = () => { throw new AssertionException("An action for Because has not been set!"); };
			Context = () => {  };
			SetUp = () => {  };
		}

		[SetUp]
		public void Setup()
		{
			SetUp.Invoke();
			var directory = Directory.GetParent(WriteRootPath);
			if(directory == null) throw new Exception(String.Format("Write root path \"{0}\" could not be located.", WriteRootPath));
			directory.GetFiles().Each(file => file.Delete());
			Context();
			Because();
		}

		protected Action Context;
		protected Action SetUp;
		protected Action Because;

		public string GetReadFilePath(string relativePath)
		{
			return ReadRootPath.AppendPaths(relativePath);
		}

		public string GetWriteFilePath(string fileName)
		{
			return WriteRootPath.AppendPaths(fileName);
		}

		public string ReadFileContents(string filepath)
		{
			return File.ReadAllText(filepath);
		}

		protected string ReadRootPath { get; private set; }
		protected string WriteRootPath { get; private set; }
	}
}