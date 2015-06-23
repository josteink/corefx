// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.Contracts;

using uid_t = System.UInt32;

using System.Security;




public static class Program
{
        public static void Main(string[] args)
        {
                var drives = System.IO.DriveInfo.GetDrives();
                foreach (var drive in drives)
                {
                        Console.WriteLine(drive.ToString());
                }
        }
}

// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

internal static partial class Interop
{
    private static partial class Libraries
    {
        /// <summary>
        /// We aren't OS X so don't have an INODE64 suffix to entry points
        /// </summary>
        internal const string INODE64SUFFIX = "";

        internal const string LibRt = "librt";  // POSIX Realtime Extensions library
        internal const string libc = "libc";    // C runtime
    }
}

// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

internal static partial class Interop
{
    /// <summary>Common Unix errno error codes.</summary>
    internal static partial class Errors
    {
        // These values were defined in:
        // include/errno.h

        internal const int EDEADLK = 11;

        internal const int EAGAIN = 35;
        internal const int EWOULDBLOCK = EAGAIN;
        internal const int EINPROGRESS = 36;
        internal const int EALREADY = 37;
        internal const int ENOTSOCK = 38;
        internal const int EDESTADDRREQ = 39;
        internal const int EMSGSIZE = 40;
        internal const int EPROTOTYPE = 41;
        internal const int ENOPROTOOPT = 42;
        internal const int EPROTONOSUPPORT = 43;
        internal const int ESOCKTNOSUPPORT = 44;
        internal const int EOPNOTSUPP = 45;
        internal const int ENOTSUP = EOPNOTSUPP;
        internal const int EPFNOSUPPORT = 46;
        internal const int EAFNOSUPPORT = 47;
        internal const int EADDRINUSE = 48;
        internal const int EADDRNOTAVAIL = 49;
        internal const int ENETDOWN = 50;
        internal const int ENETUNREACH = 51;
        internal const int ENETRESET = 52;
        internal const int ECONNABORTED = 53;
        internal const int ECONNRESET = 54;
        internal const int ENOBUFS = 55;
        internal const int EISCONN = 56;
        internal const int ENOTCONN = 57;
        internal const int ESHUTDOWN = 58;
        internal const int ETOOMANYREFS = 59;
        internal const int ETIMEDOUT = 60;
        internal const int ECONNREFUSED = 61;
        internal const int ELOOP = 62;
        internal const int ENAMETOOLONG = 63;
        internal const int EHOSTDOWN = 64;
        internal const int EHOSTUNREACH = 65;
        internal const int ENOTEMPTY = 66;
        internal const int EPROCLIM = 67;
        internal const int EUSERS = 68;
        internal const int EDQUOT = 69;
        internal const int ESTALE = 70;
        internal const int EREMOTE = 71;
        internal const int EBADRPC = 72;
        internal const int ERPCMISMATCH = 73;
        internal const int EPROGUNAVAIL = 74;
        internal const int EPROGMISMATCH = 75;
        internal const int EPROCUNAVAIL = 76;
        internal const int ENOLCK = 77;
        internal const int ENOSYS = 78;
        internal const int EFTYPE = 79;
        internal const int EAUTH = 80;
        internal const int ENEEDAUTH = 81;
        internal const int EIDRM = 82;
        internal const int ENOMSG = 83;
        internal const int EOVERFLOW = 84;
        internal const int ECANCELED = 85;
        internal const int EILSEQ = 86;
        internal const int ENOATTR = 87;
        internal const int EDOOFUS = 88;
        internal const int EBADMSG = 89;
        internal const int EMULTIHOP = 90;
        internal const int ENOLINK = 91;
        internal const int EPROTO = 92;
        internal const int ENOTCAPABLE = 93;
        internal const int ECAPMODE = 94;
        internal const int ENOTRECOVERABLE = 95;
        internal const int EOWNERDEAD = 96;
        internal const int ELAST = 96;
    }
}

// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

internal static partial class Interop
{
    /// <summary>Common Unix errno error codes.</summary>
    internal static partial class Errors
    {
        // These values were defined in:
        // include/asm-generic/errno-base.h
        // /usr/include/sys/errno.h

        internal const int EPERM = 1;
        internal const int ENOENT = 2;
        internal const int ESRCH = 3;
        internal const int EINTR = 4;
        internal const int EIO = 5;
        internal const int ENXIO = 6;
        internal const int E2BIG = 7;
        internal const int ENOEXEC = 8;
        internal const int EBADF = 9;
        internal const int ECHILD = 10;

        internal const int ENOMEM = 12;
        internal const int EACCES = 13;
        internal const int EFAULT = 14;
        internal const int ENOTBLK = 15;
        internal const int EBUSY = 16;
        internal const int EEXIST = 17;
        internal const int EXDEV = 18;
        internal const int ENODEV = 19;
        internal const int ENOTDIR = 20;
        internal const int EISDIR = 21;
        internal const int EINVAL = 22;
        internal const int ENFILE = 23;
        internal const int EMFILE = 24;
        internal const int ENOTTY = 25;
        internal const int ETXTBSY = 26;
        internal const int EFBIG = 27;
        internal const int ENOSPC = 28;
        internal const int ESPIPE = 29;
        internal const int EROFS = 30;
        internal const int EMLINK = 31;
        internal const int EPIPE = 32;
        internal const int EDOM = 33;
        internal const int ERANGE = 34;
    }
}



// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace System.IO
{
    public sealed partial class DriveInfo
    {
        public static unsafe DriveInfo[] GetDrives()
        {
            DriveInfo[] drives = null;
            Interop.libc.statfs* pBuffer = null;
            int count = Interop.libc.getmntinfo(&pBuffer, 0);
            if (count > 0)
            {
                drives = new DriveInfo[count];
                for (int i = 0; i < count; i++)
                {
                    String mountPoint = Marshal.PtrToStringAnsi((IntPtr)pBuffer[i].f_mntonname);
                    drives[i] = new DriveInfo(mountPoint);
                }
            }

            return drives;
        }

        // -----------------------------
        // ---- PAL layer ends here ----
        // -----------------------------
    }
}

// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.


internal static partial class Interop
{
    internal static partial class libc
    {
        [DllImport("libc")]
        static extern int printf(string data);
        /// <summary>
        /// Gets all the current mount points on the system
        /// </summary>
        /// <param name="ppBuffer">A pointer to an array of mount points</param>
        /// <param name="flags">Flags that are passed to the getfsstat call for each statfs struct</param>
        /// <returns>Returns the number of retrieved statfs structs</returns>
        /// <remarks
        /// Do NOT free this memory...this memory is allocated by the OS, which is responsible for it.
        /// This call could also block for a bit to wait for slow network drives.
        /// </remarks>
        [DllImport(Interop.Libraries.libc, EntryPoint = "getmntinfo" + Interop.Libraries.INODE64SUFFIX, SetLastError = true)]
        internal static unsafe extern int getmntinfo(statfs** ppBuffer, int flags);

        /// <summary>
        /// Gets a statfs struct for the given path that describes that mount point
        /// </summary>
        /// <param name="path">The path to retrieve the statfs for</param>
        /// <param name="buffer">The output statfs struct describing the mount point</param>
        /// <returns>Returns 0 on success, -1 on failure</returns>
        [DllImport(Interop.Libraries.libc, EntryPoint = "statfs" + Interop.Libraries.INODE64SUFFIX, SetLastError = true)]
        private static unsafe extern int get_statfs(string path, statfs* buffer);

        /// <summary>
        /// Gets a statfs struct for a given mount point
        /// </summary>
        /// <param name="name">The drive name to retrieve the statfs data for</param>
        /// <returns>Returns </returns>
        internal static unsafe statfs GetStatFsForDriveName(string name)
        {
            statfs data = default(statfs);
            int result = get_statfs(name, &data);
            if (result < 0)
            {
                int errno = Marshal.GetLastWin32Error();
                if (errno == Interop.Errors.ENOENT)
                        throw new System.IO.DriveNotFoundException(name); //SR.Format(SR.IO_DriveNotFound_Drive, name)); // match Win32 exception
                else
                        throw new Exception(errno.ToString()); //)Interop.GetExceptionForIoErrno(errno, isDirectory: true);
            }
            else
            {
                printf(System.Runtime.InteropServices.Marshal.SizeOf<statfs>().ToString() + "\r\n");
                printf("Should be here\r\n");
                return data;
            }
        }
    }
}


namespace System.IO
{
    public sealed partial class DriveInfo
    {
        private readonly String _name;

        public DriveInfo(String driveName)
        {
            if (driveName == null)
            {
                throw new ArgumentNullException("driveName");
            }
            Contract.EndContractBlock();

            _name = NormalizeDriveName(driveName);
        }

        public String Name
        {
            get { return _name; }
        }

        public bool IsReady
        {
            get { return Directory.Exists(Name); }
        }

        public DirectoryInfo RootDirectory
        {
            get { return new DirectoryInfo(Name); }
        }

        public override String ToString()
        {
            return Name;
        }
    }
}

// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace System.IO
{
    public sealed partial class DriveInfo
    {
        private static string NormalizeDriveName(string driveName)
        {
            if (driveName.Contains("\0"))
            {
                throw new ArgumentException(driveName, "driveName");
            }
            if (driveName.Length == 0)
            {
                    throw new ArgumentException(driveName, "driveName");
            }
            return driveName;
        }

        public DriveType DriveType
        {
            [SecuritySafeCritical]
            get
            {
                Interop.libc.statfs data = Interop.libc.GetStatFsForDriveName(Name);
                return GetDriveType(Interop.libc.GetMountPointFsType(data));
            }
        }

        public string DriveFormat
        {
            [SecuritySafeCritical]
            get
            {
                Interop.libc.statfs data = Interop.libc.GetStatFsForDriveName(Name);
                return Interop.libc.GetMountPointFsType(data);
            }
        }

        public long AvailableFreeSpace
        {
            [SecuritySafeCritical]
            get
            {
                Interop.libc.statfs data = Interop.libc.GetStatFsForDriveName(Name);
                return (long)data.f_bsize * (long)data.f_bavail;
            }
        }

        public long TotalFreeSpace
        {
            [SecuritySafeCritical]
            get
            {
                Interop.libc.statfs data = Interop.libc.GetStatFsForDriveName(Name);
                return (long)data.f_bsize * (long)data.f_bfree;
            }
        }

        public long TotalSize
        {
            [SecuritySafeCritical]
            get
            {
                Interop.libc.statfs data = Interop.libc.GetStatFsForDriveName(Name);
                return (long)data.f_bsize * (long)data.f_blocks;
            }
        }

        public String VolumeLabel
        {
            [SecuritySafeCritical]
            get
            {
                return Name;
            }
            [SecuritySafeCritical]
            set
            {
                throw new PlatformNotSupportedException();
            }
        }

        // -----------------------------
        // ---- PAL layer ends here ----
        // -----------------------------

        /// <summary>Categorizes a file system name into a drive type.</summary>
        /// <param name="fileSystemName">The name to categorize.</param>
        /// <returns>The recognized drive type.</returns>
        private static DriveType GetDriveType(string fileSystemName)
        {
            // This list is based primarily on "man fs", "man mount", "mntent.h", "/proc/filesystems",
            // and "wiki.debian.org/FileSystem". It can be extended over time as we 
            // find additional file systems that should be recognized as a particular drive type.
            switch (fileSystemName)
            {
                case "iso":
                case "isofs":
                case "iso9660":
                case "fuseiso":
                case "fuseiso9660":
                case "umview-mod-umfuseiso9660":
                    return DriveType.CDRom;

                case "adfs":
                case "affs":
                case "befs":
                case "bfs":
                case "btrfs":
                case "ecryptfs":
                case "efs":
                case "ext":
                case "ext2":
                case "ext2_old":
                case "ext3":
                case "ext4":
                case "ext4dev":
                case "fat":
                case "fuseblk":
                case "fuseext2":
                case "fusefat":
                case "hfs":
                case "hfsplus":
                case "hpfs":
                case "jbd":
                case "jbd2":
                case "jfs":
                case "jffs":
                case "jffs2":
                case "minix":
                case "minix_old":
                case "minix2":
                case "minix2v2":
                case "msdos":
                case "ocfs2":
                case "omfs":
                case "openprom":
                case "ntfs":
                case "qnx4":
                case "reiserfs":
                case "squashfs":
                case "swap":
                case "sysv":
                case "ubifs":
                case "udf":
                case "ufs":
                case "umsdos":
                case "umview-mod-umfuseext2":
                case "xenix":
                case "xfs":
                case "xiafs":
                case "xmount":
                case "zfs-fuse":
                    return DriveType.Fixed;

                case "9p":
                case "autofs":
                case "autofs4":
                case "beaglefs":
                case "cifs":
                case "coda":
                case "coherent":
                case "curlftpfs":
                case "davfs2":
                case "dlm":
                case "flickrfs":
                case "fusedav":
                case "fusesmb":
                case "gfs2":
                case "glusterfs-client":
                case "gmailfs":
                case "kafs":
                case "ltspfs":
                case "ncpfs":
                case "nfs":
                case "nfs4":
                case "obexfs":
                case "s3ql":
                case "smb":
                case "smbfs":
                case "sshfs":
                case "sysfs":
                case "sysv2":
                case "sysv4":
                case "vxfs":
                case "wikipediafs":
                    return DriveType.Network;

                case "anon_inodefs":
                case "aptfs":
                case "avfs":
                case "bdev":
                case "binfmt_misc":
                case "cgroup":
                case "configfs":
                case "cramfs":
                case "cryptkeeper":
                case "cpuset":
                case "debugfs":
                case "devfs":
                case "devpts":
                case "devtmpfs":
                case "encfs":
                case "fuse":
                case "fuse.gvfsd-fuse":
                case "fusectl":
                case "hugetlbfs":
                case "libpam-encfs":
                case "ibpam-mount":
                case "mtpfs":
                case "mythtvfs":
                case "mqueue":
                case "pipefs":
                case "plptools":
                case "proc":
                case "pstore":
                case "pytagsfs":
                case "ramfs":
                case "rofs":
                case "romfs":
                case "rootfs":
                case "securityfs":
                case "sockfs":
                case "tmpfs":
                    return DriveType.Ram;

                case "gphotofs":
                case "usbfs":
                case "usbdevice":
                case "vfat":
                    return DriveType.Removable;

                case "aufs": // marking all unions as unknown
                case "funionfs":
                case "unionfs-fuse":
                case "mhddfs":
                default:
                    return DriveType.Unknown;
            }
        }
    }
}

// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.


internal static partial class Interop
{
    internal static partial class libc
    {
        internal const int MFSNAMELEN = 16; 	     /* length of type name including null */
        internal const int MNAMELEN = 88;    	     /* size of on/from name bufs */

        [StructLayout(LayoutKind.Sequential)]
        internal unsafe struct fsid_t
        {
            internal fixed int val[2];
        }

        [StructLayout(LayoutKind.Sequential)]
        internal unsafe struct statfs
        {
            internal uint f_version;                        /* structure version number */
            internal uint f_type;                           /* type of filesystem */
            internal ulong f_flags;                         /* copy of mount exported flags */
            internal ulong f_bsize;                         /* filesystem fragment size */
            internal ulong f_iosize;                        /* optimal transfer block size */
            internal ulong f_blocks;                        /* total data blocks in filesystem */
            internal ulong f_bfree;                         /* free blocks in filesystem */
            internal long f_bavail;                         /* free blocks avail to non-superuser */
            internal ulong f_files;                         /* total file nodes in filesystem */
            internal long f_ffree;                          /* free nodes avail to non-superuser */
            internal ulong f_syncwrites;                    /* count of sync writes since mount */
            internal ulong f_asyncwrites;                   /* count of async writes since mount */
            internal ulong f_syncreads;                     /* count of sync reads since mount */
            internal ulong f_asyncreads;                    /* count of async reads since mount */
            internal fixed ulong f_spare[10];               /* unused spare */
            internal uint f_namemax;                        /* maximum filename length */
            internal uid_t f_owner;                         /* user that mounted the filesystem */
            internal fsid_t f_fsid;                         /* filesystem id */
            internal fixed byte f_charspare[80];            /* spare string space */
            internal fixed byte f_fstypename[MFSNAMELEN];   /* filesystem type name */
            internal fixed byte f_mntfromname[MNAMELEN];    /* mounted filesystem */
            internal fixed byte f_mntonname[MNAMELEN];      /* directory on which mounted */
        }

        internal static unsafe String GetMountPointFsType(statfs data)
        {
            return Marshal.PtrToStringAnsi((IntPtr)data.f_fstypename);
        }
    }
}