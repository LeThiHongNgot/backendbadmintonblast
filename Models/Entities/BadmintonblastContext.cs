using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BadmintonBlast.Models.Entities;

public partial class BadmintonblastContext : DbContext
{
    public BadmintonblastContext()
    {
    }

    public BadmintonblastContext(DbContextOptions<BadmintonblastContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bill> Bills { get; set; }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Coupon> Coupons { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Field> Fields { get; set; }

    public virtual DbSet<Hourlyrate> Hourlyrates { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<Kindproduct> Kindproducts { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Otp> Otps { get; set; }

    public virtual DbSet<Preview> Previews { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Productstock> Productstocks { get; set; }

    public virtual DbSet<Racketspecification> Racketspecifications { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-SPFVRCD;Initial Catalog=BADMINTONBLAST;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bill>(entity =>
        {
            entity.HasKey(e => e.Idbill);

            entity.ToTable("BILL");

            entity.Property(e => e.Idbill).HasColumnName("IDBILL");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("ADDRESS");
            entity.Property(e => e.Coupon).HasColumnName("COUPON");
            entity.Property(e => e.Dateorder)
                .HasColumnType("datetime")
                .HasColumnName("DATEORDER");
            entity.Property(e => e.Idcoupon).HasColumnName("IDCOUPON");
            entity.Property(e => e.Idcustomer).HasColumnName("IDCUSTOMER");
            entity.Property(e => e.Message)
                .HasColumnType("text")
                .HasColumnName("MESSAGE");
            entity.Property(e => e.Namecustomer)
                .HasMaxLength(255)
                .HasColumnName("NAMECUSTOMER");
            entity.Property(e => e.Pay)
                .HasMaxLength(50)
                .HasColumnName("PAY");
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .HasColumnName("PHONE");
            entity.Property(e => e.Status).HasColumnName("STATUS");
            entity.Property(e => e.Totalamount).HasColumnName("TOTALAMOUNT");
            entity.Property(e => e.Transactioncode)
                .HasMaxLength(50)
                .HasColumnName("TRANSACTIONCODE");

            entity.HasOne(d => d.IdcouponNavigation).WithMany(p => p.Bills)
                .HasForeignKey(d => d.Idcoupon)
                .HasConstraintName("FK_BILL_COUPON");

            entity.HasOne(d => d.IdcustomerNavigation).WithMany(p => p.Bills)
                .HasForeignKey(d => d.Idcustomer)
                .HasConstraintName("FK_BILL_CUSTOMER");
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.Idbrand);

            entity.ToTable("BRAND");

            entity.Property(e => e.Idbrand).HasColumnName("IDBRAND");
            entity.Property(e => e.Description)
                .HasColumnType("ntext")
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.Image).HasMaxLength(255);
            entity.Property(e => e.Namebrand)
                .HasMaxLength(255)
                .HasColumnName("NAMEBRAND");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.Idcart);

            entity.ToTable("CART");

            entity.HasIndex(e => new { e.Idproduct, e.Idcustomer }, "UQ_Cart_Product_Customer").IsUnique();

            entity.Property(e => e.Idcart).HasColumnName("IDCART");
            entity.Property(e => e.Color).HasMaxLength(50);
            entity.Property(e => e.Idcustomer).HasColumnName("IDCUSTOMER");
            entity.Property(e => e.Idproduct).HasColumnName("IDPRODUCT");
            entity.Property(e => e.Quatity).HasColumnName("QUATITY");
            entity.Property(e => e.Size).HasMaxLength(50);

            entity.HasOne(d => d.IdcustomerNavigation).WithMany(p => p.Carts)
                .HasForeignKey(d => d.Idcustomer)
                .HasConstraintName("FK_CART_CUSTOMER");

            entity.HasOne(d => d.IdproductNavigation).WithMany(p => p.Carts)
                .HasForeignKey(d => d.Idproduct)
                .HasConstraintName("FK_CART_PRODUCT");
        });

        modelBuilder.Entity<Coupon>(entity =>
        {
            entity.HasKey(e => e.Idcoupon);

            entity.ToTable("COUPON");

            entity.Property(e => e.Idcoupon).HasColumnName("IDCOUPON");
            entity.Property(e => e.Enddate)
                .HasColumnType("datetime")
                .HasColumnName("ENDDATE");
            entity.Property(e => e.Promotion).HasColumnName("PROMOTION");
            entity.Property(e => e.Quality).HasColumnName("QUALITY");
            entity.Property(e => e.Startdate)
                .HasColumnType("datetime")
                .HasColumnName("STARTDATE");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Idcustomer);

            entity.ToTable("CUSTOMER");

            entity.Property(e => e.Idcustomer).HasColumnName("IDCUSTOMER");
            entity.Property(e => e.District)
                .HasMaxLength(255)
                .HasColumnName("DISTRICT");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Hamlet)
                .HasMaxLength(255)
                .HasColumnName("HAMLET");
            entity.Property(e => e.ImageCustomer).HasMaxLength(255);
            entity.Property(e => e.Namecustomer)
                .HasMaxLength(255)
                .HasColumnName("NAMECUSTOMER");
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .HasColumnName("PHONE");
            entity.Property(e => e.Province)
                .HasMaxLength(255)
                .HasColumnName("PROVINCE");
            entity.Property(e => e.Role).HasMaxLength(50);
            entity.Property(e => e.Status).HasDefaultValue(false);
            entity.Property(e => e.Village)
                .HasMaxLength(255)
                .HasColumnName("VILLAGE");

            entity.HasOne(d => d.EmailNavigation).WithMany(p => p.Customers)
                .HasForeignKey(d => d.Email)
                .HasConstraintName("FK_OTPS_CUSTOMER");
        });

        modelBuilder.Entity<Field>(entity =>
        {
            entity.HasKey(e => e.Idfield);

            entity.ToTable("FIELDS");

            entity.Property(e => e.Idfield).HasColumnName("IDFIELD");
            entity.Property(e => e.Fieldname)
                .HasMaxLength(256)
                .HasColumnName("FIELDNAME");
        });

        modelBuilder.Entity<Hourlyrate>(entity =>
        {
            entity.HasKey(e => e.Idhourlyrates);

            entity.ToTable("HOURLYRATES");

            entity.Property(e => e.Idhourlyrates).HasColumnName("IDHOURLYRATES");
            entity.Property(e => e.Endtimerates).HasColumnName("ENDTIMERATES");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("PRICE");
            entity.Property(e => e.Starttimerates).HasColumnName("STARTTIMERATES");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.ToTable("IMAGE");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Idproduct).HasColumnName("IDPRODUCT");
            entity.Property(e => e.Image4)
                .HasMaxLength(255)
                .HasColumnName("IMAGE4");

            entity.HasOne(d => d.IdproductNavigation).WithMany(p => p.Images)
                .HasForeignKey(d => d.Idproduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IMAGE_PRODUCT");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.Idinvoice);

            entity.ToTable("INVOICES");

            entity.Property(e => e.Idinvoice).HasColumnName("IDINVOICE");
            entity.Property(e => e.Customername)
                .HasMaxLength(255)
                .HasColumnName("CUSTOMERNAME");
            entity.Property(e => e.Customerphone)
                .HasMaxLength(50)
                .HasColumnName("CUSTOMERPHONE");
            entity.Property(e => e.Idcustomer).HasColumnName("IDCUSTOMER");
            entity.Property(e => e.Paymentmethod)
                .HasMaxLength(50)
                .HasColumnName("PAYMENTMETHOD");
            entity.Property(e => e.Reservationdate)
                .HasColumnType("datetime")
                .HasColumnName("RESERVATIONDATE");
            entity.Property(e => e.Totalamount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("TOTALAMOUNT");
            entity.Property(e => e.Transactioncode)
                .HasMaxLength(50)
                .HasColumnName("TRANSACTIONCODE");

            entity.HasOne(d => d.IdcustomerNavigation).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.Idcustomer)
                .HasConstraintName("FK_INVOICES_CUSTOMER");
        });

        modelBuilder.Entity<Kindproduct>(entity =>
        {
            entity.HasKey(e => e.Idkindproduct);

            entity.ToTable("KINDPRODUCT");

            entity.Property(e => e.Idkindproduct).HasColumnName("IDKINDPRODUCT");
            entity.Property(e => e.Image).HasMaxLength(255);
            entity.Property(e => e.Nameproduct)
                .HasMaxLength(255)
                .HasColumnName("NAMEPRODUCT");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Idorder);

            entity.ToTable("ORDER");

            entity.Property(e => e.Idorder).HasColumnName("IDORDER");
            entity.Property(e => e.Color)
                .HasMaxLength(256)
                .HasColumnName("COLOR");
            entity.Property(e => e.DateOrder).HasColumnType("datetime");
            entity.Property(e => e.Idbill).HasColumnName("IDBILL");
            entity.Property(e => e.Idproduct).HasColumnName("IDPRODUCT");
            entity.Property(e => e.Nameproduct)
                .HasMaxLength(255)
                .HasColumnName("NAMEPRODUCT");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("PRICE");
            entity.Property(e => e.Quatity).HasColumnName("QUATITY");
            entity.Property(e => e.Size)
                .HasMaxLength(256)
                .HasColumnName("SIZE");

            entity.HasOne(d => d.IdbillNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Idbill)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BILL_PRODUCT");

            entity.HasOne(d => d.IdproductNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Idproduct)
                .HasConstraintName("FK_ORDER_PRODUCT");
        });

        modelBuilder.Entity<Otp>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("PK_Email");

            entity.ToTable("OTPs");

            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("EMAIL");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Expiration).HasColumnType("datetime");
            entity.Property(e => e.Otp1)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("OTP");
        });

        modelBuilder.Entity<Preview>(entity =>
        {
            entity.HasKey(e => e.Idpreview);

            entity.ToTable("PREVIEW");

            entity.Property(e => e.Idpreview).HasColumnName("IDPREVIEW");
            entity.Property(e => e.Comment)
                .HasColumnType("ntext")
                .HasColumnName("COMMENT");
            entity.Property(e => e.Dateprevew)
                .HasColumnType("datetime")
                .HasColumnName("DATEPREVEW");
            entity.Property(e => e.Idcustomer).HasColumnName("IDCUSTOMER");
            entity.Property(e => e.Idproduct).HasColumnName("IDPRODUCT");
            entity.Property(e => e.Preview1).HasColumnName("PREVIEW");

            entity.HasOne(d => d.IdcustomerNavigation).WithMany(p => p.Previews)
                .HasForeignKey(d => d.Idcustomer)
                .HasConstraintName("FK_PREVIEW_CUSTOMER");

            entity.HasOne(d => d.IdproductNavigation).WithMany(p => p.Previews)
                .HasForeignKey(d => d.Idproduct)
                .HasConstraintName("FK_PREVIEW_PRODUCT");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Idproduct);

            entity.ToTable("PRODUCTS");

            entity.Property(e => e.Idproduct).HasColumnName("IDPRODUCT");
            entity.Property(e => e.Available).HasColumnName("AVAILABLE");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("DATE");
            entity.Property(e => e.Deprice).HasColumnName("DEPRICE");
            entity.Property(e => e.Description)
                .HasColumnType("ntext")
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.Idbrand).HasColumnName("IDBRAND");
            entity.Property(e => e.Idkindproduct).HasColumnName("IDKINDPRODUCT");
            entity.Property(e => e.Kindproduct)
                .HasMaxLength(255)
                .HasColumnName("KINDPRODUCT");
            entity.Property(e => e.Namebrand)
                .HasMaxLength(255)
                .HasColumnName("NAMEBRAND");
            entity.Property(e => e.Nameproduct)
                .HasMaxLength(255)
                .HasColumnName("NAMEPRODUCT");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("PRICE");

            entity.HasOne(d => d.IdbrandNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.Idbrand)
                .HasConstraintName("FK_PRODUCT_BRAND");

            entity.HasOne(d => d.IdkindproductNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.Idkindproduct)
                .HasConstraintName("FK_PRODUCT_KINDPRODUCT");
        });

        modelBuilder.Entity<Productstock>(entity =>
        {
            entity.ToTable("PRODUCTSTOCK");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Idproduct).HasColumnName("IDPRODUCT");
            entity.Property(e => e.Namecolor)
                .HasMaxLength(255)
                .HasColumnName("NAMECOLOR");
            entity.Property(e => e.Namesize)
                .HasMaxLength(255)
                .HasColumnName("NAMESIZE");
            entity.Property(e => e.Quatity).HasColumnName("QUATITY");

            entity.HasOne(d => d.IdproductNavigation).WithMany(p => p.Productstocks)
                .HasForeignKey(d => d.Idproduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PRODUCTSTOCK_PRODUCT");
        });

        modelBuilder.Entity<Racketspecification>(entity =>
        {
            entity.HasKey(e => e.Idproduct);

            entity.ToTable("RACKETSPECIFICATIONS");

            entity.Property(e => e.Idproduct)
                .ValueGeneratedNever()
                .HasColumnName("IDPRODUCT");
            entity.Property(e => e.Balance)
                .HasMaxLength(256)
                .HasColumnName("BALANCE");
            entity.Property(e => e.Color)
                .HasMaxLength(256)
                .HasColumnName("COLOR");
            entity.Property(e => e.Flexibility)
                .HasMaxLength(256)
                .HasColumnName("FLEXIBILITY");
            entity.Property(e => e.Framematerial)
                .HasMaxLength(256)
                .HasColumnName("FRAMEMATERIAL");
            entity.Property(e => e.Shaftmaterial)
                .HasMaxLength(256)
                .HasColumnName("SHAFTMATERIAL");
            entity.Property(e => e.Stringtension)
                .HasMaxLength(256)
                .HasColumnName("STRINGTENSION");
            entity.Property(e => e.Weight)
                .HasMaxLength(256)
                .HasColumnName("WEIGHT");

            entity.HasOne(d => d.IdproductNavigation).WithOne(p => p.Racketspecification)
                .HasForeignKey<Racketspecification>(d => d.Idproduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RACKETSPECIFICATIONS_PRODUCT");
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.Idreservation);

            entity.ToTable("RESERVATIONS");

            entity.Property(e => e.Idreservation).HasColumnName("IDRESERVATION");
            entity.Property(e => e.Endtimerates).HasColumnName("ENDTIMERATES");
            entity.Property(e => e.Fieldstatus)
                .HasMaxLength(50)
                .HasColumnName("FIELDSTATUS");
            entity.Property(e => e.Idcustomer).HasColumnName("IDCUSTOMER");
            entity.Property(e => e.Idfield).HasColumnName("IDFIELD");
            entity.Property(e => e.Idhourlyrates).HasColumnName("IDHOURLYRATES");
            entity.Property(e => e.Idinvoice).HasColumnName("IDINVOICE");
            entity.Property(e => e.MissingSlots).HasColumnName("MISSING_SLOTS");
            entity.Property(e => e.Namecustomer)
                .HasMaxLength(255)
                .HasColumnName("NAMECUSTOMER");
            entity.Property(e => e.Namefield)
                .HasMaxLength(10)
                .HasColumnName("NAMEFIELD");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("PRICE");
            entity.Property(e => e.Starttimerates).HasColumnName("STARTTIMERATES");
            entity.Property(e => e.Transactioncode)
                .HasMaxLength(50)
                .HasColumnName("TRANSACTIONCODE");

            entity.HasOne(d => d.IdfieldNavigation).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.Idfield)
                .HasConstraintName("FK_RESERVAT_FIELDS");

            entity.HasOne(d => d.IdhourlyratesNavigation).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.Idhourlyrates)
                .HasConstraintName("FK_RESERVAT_HOURLYRAT");

            entity.HasOne(d => d.IdinvoiceNavigation).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.Idinvoice)
                .HasConstraintName("FK_INVOICES_RESERVAT");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
