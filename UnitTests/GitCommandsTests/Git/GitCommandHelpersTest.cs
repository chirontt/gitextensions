using System;
using GitUIPluginInterfaces;
            var module = new GitModule(null);
        public void TestGetDiffChangedFilesFromString()
            var module = new GitModule(null);
                var status = GitCommandHelpers.GetDiffChangedFilesFromString(module, statusString, "HEAD", GitRevision.IndexGuid, "HEAD");
                var status = GitCommandHelpers.GetDiffChangedFilesFromString(module, statusString, "HEAD", GitRevision.IndexGuid, "HEAD");
                var status = GitCommandHelpers.GetDiffChangedFilesFromString(module, statusString, "HEAD", GitRevision.IndexGuid, "HEAD");
                var status = GitCommandHelpers.GetDiffChangedFilesFromString(module, statusString, "HEAD", GitRevision.IndexGuid, "HEAD");
                // git diff -M -C -z --cached --name-status
                // Ignore unmerged (in conflict) if revision is work tree
                string statusString = "M  testfile.txt\0U  testfile.txt\0";
                var status = GitCommandHelpers.GetDiffChangedFilesFromString(module, statusString, GitRevision.IndexGuid, GitRevision.UnstagedGuid, GitRevision.IndexGuid);
                Assert.IsTrue(status.Count == 1);
                Assert.IsTrue(status[0].Name == "testfile.txt");
                Assert.IsTrue(status[0].Staged == StagedStatus.WorkTree);
            }

            {
                // git diff -M -C -z --cached --name-status
                // Include unmerged (in conflict) if revision is index
                string statusString = "M  testfile.txt\0U  testfile2.txt\0";
                var status = GitCommandHelpers.GetDiffChangedFilesFromString(module, statusString, "HEAD", GitRevision.IndexGuid, "HEAD");
                Assert.IsTrue(status.Count == 2);
                Assert.IsTrue(status[0].Name == "testfile.txt");
                Assert.IsTrue(status[0].Staged == StagedStatus.Index);
            }

            {
                // git diff -M -C -z --name-status 123 456
                // Check that the staged status is None if not Index/WorkTree
                string statusString = "M  testfile.txt\0U  testfile2.txt\0";
                var status = GitCommandHelpers.GetDiffChangedFilesFromString(module, statusString, GitRevision.IndexGuid, "456", "678");
                Assert.IsTrue(status.Count == 2);
                Assert.IsTrue(status[0].Name == "testfile.txt");
                Assert.IsTrue(status[0].Staged == StagedStatus.None);
            }

            {
                // git diff -M -C -z --name-status 123 456
                // Check that the staged status is None if not Index/WorkTree
                string statusString = "M  testfile.txt\0U  testfile2.txt\0";
                var status = GitCommandHelpers.GetDiffChangedFilesFromString(module, statusString, "123", "456", null);
                Assert.IsTrue(status.Count == 2);
                Assert.IsTrue(status[0].Name == "testfile.txt");
                Assert.IsTrue(status[0].Staged == StagedStatus.None);
            }

#if !DEBUG && false
            // This test is for documentation, but as the throw is in a called function, it will not test cleanly
            {
                // git diff -M -C -z --name-status 123 456
                // Check that the staged status is None if not Index/WorkTree
                // Assertion in Debug, throws in Release
                string statusString = "M  testfile.txt\0U  testfile2.txt\0";

                var status = GitCommandHelpers.GetDiffChangedFilesFromString(module, statusString, null, null, null);
                Assert.IsTrue(status.Count == 2);
                Assert.IsTrue(status[0].Name == "testfile.txt");
                Assert.IsTrue(status[0].Staged == StagedStatus.Unknown);
             }
#endif
        }

        [Test]
        public void TestGetStatusChangedFilesFromString()
        {
            var module = new GitModule(null);
            {
                // git status --porcelain=2 --untracked-files=no -z
                // porcelain v1: string statusString = "M  adfs.h\0M  dir.c\0";
                string statusString = "#Header\03 unknown info\01 .M S..U 160000 160000 160000 cbca134e29be13b35f21ca4553ba04f796324b1c cbca134e29be13b35f21ca4553ba04f796324b1c adfs.h\01 .M SCM. 160000 160000 160000 6bd3b036fc5718a51a0d27cde134c7019798c3ce 6bd3b036fc5718a51a0d27cde134c7019798c3ce dir.c\0\r\nwarning: LF will be replaced by CRLF in adfs.h.\nThe file will have its original line endings in your working directory.\nwarning: LF will be replaced by CRLF in dir.c.\nThe file will have its original line endings in your working directory.";
                var status = GitCommandHelpers.GetStatusChangedFilesFromString(module, statusString);

            {
                // git status --porcelain=2 --untracked-files -z
                // porcelain v1: string statusString = "M  adfs.h\0?? untracked_file\0";
                string statusString = "1 .M S..U 160000 160000 160000 cbca134e29be13b35f21ca4553ba04f796324b1c cbca134e29be13b35f21ca4553ba04f796324b1c adfs.h\0? untracked_file\0";
                var status = GitCommandHelpers.GetStatusChangedFilesFromString(module, statusString);
                Assert.IsTrue(status.Count == 2);
                Assert.IsTrue(status[0].Name == "adfs.h");
                Assert.IsTrue(status[1].Name == "untracked_file");
            }

            {
                // git status --porcelain=2 --ignored-files -z
                // porcelain v1: string statusString = ".M  adfs.h\0!! ignored_file\0";
                string statusString = "1 .M S..U 160000 160000 160000 cbca134e29be13b35f21ca4553ba04f796324b1c cbca134e29be13b35f21ca4553ba04f796324b1c adfs.h\0! ignored_file\0";
                var status = GitCommandHelpers.GetStatusChangedFilesFromString(module, statusString);
                Assert.IsTrue(status.Count == 2);
                Assert.IsTrue(status[0].Name == "adfs.h");
                Assert.IsTrue(status[1].Name == "ignored_file");
            }
            var testModule = new GitModule("C:\\Test\\SuperProject");
            var status = GitCommandHelpers.ParseSubmoduleStatus(text, testModule, fileName);
            Assert.AreEqual(ObjectId.Parse("b5a3d51777c85a9aeee534c382b5ccbb86b485d3"), status.Commit);
            Assert.AreEqual(fileName, status.Name);
            Assert.AreEqual(ObjectId.Parse("a17ea0c8ebe9d8cd7e634ba44559adffe633c11d"), status.OldCommit);
            Assert.AreEqual(fileName, status.OldName);
            status = GitCommandHelpers.ParseSubmoduleStatus(text, testModule, fileName);
            Assert.AreEqual(ObjectId.Parse("0cc457d030e92f804569407c7cd39893320f9740"), status.Commit);
            Assert.AreEqual(fileName, status.Name);
            Assert.AreEqual(ObjectId.Parse("2fb88514cfdc37a2708c24f71eca71c424b8d402"), status.OldCommit);
            Assert.AreEqual(fileName, status.OldName);
            status = GitCommandHelpers.ParseSubmoduleStatus(text, testModule, fileName);

            Assert.AreEqual(ObjectId.Parse("b5a3d51777c85a9aeee534c382b5ccbb86b485d3"), status.Commit);
            Assert.AreEqual(fileName, status.Name);
            Assert.AreEqual(ObjectId.Parse("a17ea0c8ebe9d8cd7e634ba44559adffe633c11d"), status.OldCommit);
            Assert.AreEqual("Externals/conemu-inside-a", status.OldName);

            text = "diff --git a/Externals/ICSharpCode.TextEditor b/Externals/ICSharpCode.TextEditor\r\nnew file mode 160000\r\nindex 000000000..05321769f\r\n--- /dev/null\r\n+++ b/Externals/ICSharpCode.TextEditor\r\n@@ -0,0 +1 @@\r\n+Subproject commit 05321769f039f39fa7f6748e8f30d5c8f157c7dc\r\n";
            fileName = "Externals/ICSharpCode.TextEditor";
            status = GitCommandHelpers.ParseSubmoduleStatus(text, testModule, fileName);

            Assert.AreEqual(ObjectId.Parse("05321769f039f39fa7f6748e8f30d5c8f157c7dc"), status.Commit);
            Assert.AreEqual(fileName, status.Name);
            Assert.IsNull(status.OldCommit);
            Assert.AreEqual("Externals/ICSharpCode.TextEditor", status.OldName);
                GitCommandHelpers.RebaseCmd("branch", interactive: false, preserveMerges: false, autosquash: false, autoStash: false));
                GitCommandHelpers.RebaseCmd("branch", interactive: true, preserveMerges: false, autosquash: false, autoStash: false));
                GitCommandHelpers.RebaseCmd("branch", interactive: false, preserveMerges: true, autosquash: false, autoStash: false));
                GitCommandHelpers.RebaseCmd("branch", interactive: false, preserveMerges: false, autosquash: true, autoStash: false));
                GitCommandHelpers.RebaseCmd("branch", interactive: false, preserveMerges: false, autosquash: false, autoStash: true));
                GitCommandHelpers.RebaseCmd("branch", interactive: true, preserveMerges: false, autosquash: true, autoStash: false));
                GitCommandHelpers.RebaseCmd("branch", interactive: true, preserveMerges: true, autosquash: true, autoStash: true));
                GitCommandHelpers.RebaseCmd("branch", interactive: false, preserveMerges: false, autosquash: false, autoStash: false, "from", "onto"));
                GitCommandHelpers.CleanUpCmd(dryRun: false, directories: false, nonIgnored: true, ignored: false));
                GitCommandHelpers.CleanUpCmd(dryRun: true, directories: false, nonIgnored: true, ignored: false));
                GitCommandHelpers.CleanUpCmd(dryRun: false, directories: true, nonIgnored: true, ignored: false));
                GitCommandHelpers.CleanUpCmd(dryRun: false, directories: false, nonIgnored: false, ignored: false));
                GitCommandHelpers.CleanUpCmd(dryRun: false, directories: false, nonIgnored: true, ignored: true));
                GitCommandHelpers.CleanUpCmd(dryRun: false, directories: false, nonIgnored: false, ignored: true));
                GitCommandHelpers.CleanUpCmd(dryRun: false, directories: false, nonIgnored: true, ignored: false, "paths"));
                "status --porcelain=2 -z --untracked-files --ignore-submodules",
                "status --porcelain=2 -z --untracked-files --ignore-submodules --ignored",
                "status --porcelain=2 -z --untracked-files=no --ignore-submodules",
                "status --porcelain=2 -z --untracked-files=normal --ignore-submodules",
                "status --porcelain=2 -z --untracked-files=all --ignore-submodules",
                "status --porcelain=2 -z --untracked-files --ignore-submodules=none",
                "status --porcelain=2 -z --untracked-files --ignore-submodules=none",
                "status --porcelain=2 -z --untracked-files --ignore-submodules=untracked",
                "status --porcelain=2 -z --untracked-files --ignore-submodules=dirty",
                "status --porcelain=2 -z --untracked-files --ignore-submodules=all",
            Assert.AreEqual(
                "--no-optional-locks status --porcelain=2 -z --untracked-files --ignore-submodules",
                GitCommandHelpers.GetAllChangedFilesCmd(excludeIgnoredFiles: true, UntrackedFilesMode.Default, IgnoreSubmodulesMode.Default, noLocks: true));