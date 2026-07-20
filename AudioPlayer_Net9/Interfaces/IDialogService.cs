using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AudioPlayer_Net9.Models;

namespace AudioPlayer_Net9.Interfaces {
  public interface IDialogService {
    bool? ShowMetadataDialog(Track track);
  }
}
