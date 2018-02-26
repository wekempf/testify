# Release Check-list

* On develop "git cob release/Major.Minor.Patch"
* ".\build.ps1"
  * Verify everything builds OK with proper version number.
* "git publish"
* Verify everything builds on AppVeyor
* Verify NuGet packages on MyGet
* Make further changes and repeat last 2 steps until release is "final"
* "git co master"
* "git merge release/Major.Minor.Patch"
* "git tag -a Major.Minor.Patch -m 'Major.Minor.Path'"
* ".\build.ps1"
  * Verify everything builds OK with proper version number.
* "git branch release/Major.Minor.Patch -d"
* "git push origin --delete release/Major.Minor.Patch --follow-tags"
* Verify everything builds on AppVeyor
* Verify NuGet packages on NuGet.org
* "git co develop"
* "git merge master"
* "git push"